using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Halic.Bussiness.Abstract;
using Halic.Entity;
using HalicHub.Extensions;
using HalicHub.Identity;
using HalicHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HalicHub.Controllers
{
    [Authorize]
    //BURAYA MUTLAKA YETKİLENDİRİLMİŞ GİRER DEMEK
    public class AdminController : Controller
    {
        private IArticleServices _articleServices;
        private ICategoryServices _categoryServices;
        private IAuthorServices _authorServices;
        private INewsServices _newsServices;
        private INCategoryServices _nCategoryServices;
        private IVideoServices _videoServices;
        private ISliderServices _sliderServices;
        private IActivitiesServices _activitiesServices;
        private IEMailServices _emailServices;

        private readonly IHostingEnvironment _env;

        //Rol İşlemleri

        //private RoleManager<IdentityRole> _roleManager;
        //private UserManager<User> _UserManager;

        public AdminController(IArticleServices articleServices, IEMailServices eMailServices, IHostingEnvironment env, ICategoryServices categoryServices, IAuthorServices authorServices, INewsServices newsServices, INCategoryServices nCategoryServices, IVideoServices videoServices, ISliderServices sliderServices, IActivitiesServices activitiesServices)
        {
            _articleServices = articleServices;
            _categoryServices = categoryServices;
            _authorServices = authorServices;
            _newsServices = newsServices;
            _nCategoryServices = nCategoryServices;
            _videoServices = videoServices;
            _sliderServices = sliderServices;
            _activitiesServices = activitiesServices;
            //_roleManager = IdentityRole;
            //_UserManager = userManager;
            _env = env;
            _emailServices = eMailServices;
        }
        [HttpPost]
        public IActionResult UploadImage(IFormFile upload)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();


            var path = Path.Combine(
                _env.WebRootPath, "upload/image",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }

            var url = $"{"/upload/image/"}{fileName}";

            return Json(new { uploaded = true, url });
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult ArticleListAdmin()
        {   
            return View(new ArticleListViewModel {
              Articles=_articleServices.GetOrderAll()
            });
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult ArticleCreateAdmin()
        {
            ViewBag.Categories = _categoryServices.GetAll();
            ViewBag.Authors = _authorServices.GetAll();
            return View();
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> ArticleCreateAdmin(ArticleModel model, IFormFile file,IFormCollection form)
        {
            int[] diziCategories;
            int[] diziAuthors;

            var dataCategories = Convert.ToInt32(form["categories"]);
            diziCategories = new int[] { dataCategories };

            var dataAuthors = Convert.ToInt32(form["authors"]);
            diziAuthors = new int[] { dataAuthors };

            var entity = new Article()
                {
                    Title = model.Title,
                    Content = model.Content,
                    Description = model.Description,
                    Date = model.Date,
                    Image = model.Image,
                    Url = model.Url,
                    IsApproved = model.IsApproved,
                };

                if (file != null)
                {
                    var extention = Path.GetExtension(file.FileName);
                    var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                    entity.Image = randomName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
                _articleServices.ArticleCreate(entity, diziCategories, diziAuthors);

                TempData.Put("message", new AlertMessage()
                {
                    Title = "Yeni Makale",
                    Message = $"{model.Title} Makale Başarıyla Eklendi",
                    AlertType = "success"
                });

                return RedirectToAction("ArticleListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ArticleEditAdmin(int? id)     
        {   
            var entity = _articleServices.GetByWithCategoriesId((int)id);

            var model = new ArticleModel()
            {
                ArticleId=entity.ArticleId,
                Title = entity.Title,
                Content = entity.Content,
                Description = entity.Description,
                Date = entity.Date,
                Image = entity.Image,
                Url = entity.Url,
                IsApproved = entity.IsApproved,
                Categories=entity.ArticleCategories.Select(i=>i.Categories).ToList(),
                Authors = entity.ArticleAuthors.Select(i=>i.Authors).ToList(),
                AuthorId=entity.ArticleAuthors.Select(i=>i.AuthorId).FirstOrDefault(),
                CategoryId=entity.ArticleCategories.Select(i=>i.CategoryId).FirstOrDefault()
            };
            ViewBag.Categories = _categoryServices.GetAll();
            ViewBag.Authors = _authorServices.GetAll();

            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> ArticleEditAdmin(ArticleModel model,IFormFile file, IFormCollection form)
        {
            int[] diziCategories;
            int[] diziAuthors;

            var dataCategories = Convert.ToInt32(form["categories"]);
            diziCategories = new int[] { dataCategories };

            var dataAuthors = Convert.ToInt32(form["authors"]);
            diziAuthors = new int[] { dataAuthors };

            
                var entity = _articleServices.GetById(model.ArticleId);

            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.Description = model.Description;
            entity.Date = model.Date;
            entity.Url = model.Url;
            entity.IsApproved = model.IsApproved;

            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            _articleServices.Update(entity,diziCategories,diziAuthors);

            TempData.Put("message", new AlertMessage()
            {
                Title = "Makale Güncellendi",
                Message = $"{model.Title} Makale Başarıyla Güncellendi",
                AlertType = "warning"
            });
            return RedirectToAction("ArticleListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult ArticleDeleteAdmin(int ArticleId)
        {
            var entity = _articleServices.GetById(ArticleId);
            var tittle = entity.Title;
            _articleServices.Delete(entity);

            TempData.Put("message", new AlertMessage()
            {
                Title = "Makale Silindi",
                Message = $"{tittle} Makale Başarıyla Silindi",
                AlertType = "danger"
            });
            return RedirectToAction("ArticleListAdmin");
        }
        //-----------------------------------------------------------
        //MakaleKategori sayfası
        [Authorize(Roles = "admin")]
        public IActionResult ArticleCategoryListAdmin()
        {
            return View(new CategoryListViewModel
            {
                Categories = _categoryServices.GetAll()
            });
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ArticleCategoryCreateAdmin()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> ArticleCategoryCreateAdmin(CategoryModel model, IFormFile file)
        {
            var entity = new Category()
            {
                Tittle = model.Tittle,
                Url = model.Url,
            };
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _categoryServices.Create(entity);

            TempData.Put("message", new AlertMessage()
            {
                Title = "Yeni Kategori",
                Message = $"{entity.Tittle} Kategorisi Başarıyla Eklendi",
                AlertType = "success"
            });
            return RedirectToAction("ArticleCategoryListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ArticleCategoryEditAdmin(int id)
        {
            var entity = _categoryServices.GetById(id);
            var model = new CategoryModel()
            {
                CategoryId = entity.CategoryId,
                Tittle = entity.Tittle,
                Url = entity.Url,
                Image = entity.Image
            };
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> ArticleCategoryEditAdmin(CategoryModel model, IFormFile file)
        {
            var entity = _categoryServices.GetById(model.CategoryId);
            entity.CategoryId = model.CategoryId;
            entity.Tittle = model.Tittle;
            entity.Url = model.Url;

            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _categoryServices.Update(entity);
            TempData.Put("message", new AlertMessage()
            {
                Title = "Kategori Gümcelleme",
                Message = $"{entity.Tittle} Kategorisi Başarıyla Güncellendi",
                AlertType = "warning"
            });
            return RedirectToAction("ArticleCategoryListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult ArticleCategoryDeleteAdmin(int CategoryId)
        {
            var entity = _categoryServices.GetById(CategoryId);
            _categoryServices.Delete(entity);
            var name = entity.Tittle;
            TempData.Put("message", new AlertMessage()
            {
                Title = "Kategori Silme",
                Message = $"{name} Kategorisi Başarıyla Silindi",
                AlertType = "danger"
            });
            return RedirectToAction("ArticleCategoryListAdmin");
        }
        //----------------------------------------------------------------------------------------
        // Yazar Operasyonları  AUTHOR
        [Authorize(Roles = "admin")]
        public IActionResult AuthorListAdmin()              
        {
            return View(new AuthorListViewModel
            {
                Authors = _authorServices.GetOrderAll() 
            });
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AuthorCreateAdmin()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AuthorCreateAdmin(AuthorModel model, IFormFile file)
        {
            var entity = new Author()
            {
                NameSurname = model.NameSurname,
                Description = model.Description,
                Content=model.Content,
                Url = model.Url,
                Twitter=model.Twitter,
                Linkedin=model.Linkedin
            };
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _authorServices.Create(entity);//burada file vermene gerek yok cshtmlde enctype multipleformdata yaptık bu file dosyasını servera taşı demek
            TempData.Put("message", new AlertMessage()
            {
                Title = "Yeni Yazar Ekleme",
                Message = $"{model.NameSurname} Başarıyla Eklendi",
                AlertType = "success"
            });
            return RedirectToAction("AuthorListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult AuthorEditAdmin(int? id)   
        {
            var entity = _authorServices.GetById((int) id);

            var model = new AuthorModel()
            { 
                AuthorId=entity.AuthorId,
                NameSurname = entity.NameSurname,
                Description = entity.Description,
                Content=entity.Content,
                Url=entity.Url,
                Image = entity.Image,
                Twitter= entity.Twitter,
                Linkedin= entity.Linkedin
            };
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AuthorEditAdmin(AuthorModel model, IFormFile file)         
        {
            var entity = _authorServices.GetById(model.AuthorId);

            entity.AuthorId = model.AuthorId;
            entity.NameSurname = model.NameSurname;
            entity.Description = model.Description;
            entity.Content = model.Content;
            entity.Url = model.Url;
            entity.Twitter = model.Twitter;
            entity.Linkedin = model.Linkedin;

            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _authorServices.Update(entity);
            TempData.Put("message", new AlertMessage()
            {
                Title = "Yazar Güncelleme",
                Message = $"{model.NameSurname} Yazarı Başarıyla Güncellendi",
                AlertType = "warning"
            });
            return RedirectToAction("AuthorListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult AuthorDeleteAdmin(int AuthorId)   //authorId list.cshtmldeki alandada yazıyor ben buraya id diyodum ve silmiyodu sonra anladımki cshtmldeki name alanına ne yzarsam burayada onu yazmalıyım  
        {
            var entity = _authorServices.GetById(AuthorId);
            var name = entity.NameSurname;
            _authorServices.Delete(entity);
            TempData.Put("message", new AlertMessage()
            {
                Title = "Yazar Silme",
                Message = $"{name} Yazarı Başarıyla Silindi",
                AlertType = "danger"
            });
            return RedirectToAction("AuthorListAdmin");
        }
        //----------------- Haberler ------------------------
        [Authorize(Roles = "user")]
        public IActionResult NewsListAdmin()        
        {
            return View(new NewsListViewModel
            {
                News=_newsServices.GetOrderAll()    
            });
        }
        [Authorize(Roles = "user")]
        [HttpGet]
        public IActionResult NewsCreateAdmin()         
        {
            ViewBag.NCategories = _nCategoryServices.GetAll();                  
            ViewBag.Authors = _authorServices.GetAll();
            return View();
        }
        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> NewsCreateAdmin(ArticleModel model, IFormFile file,IFormCollection form)
        {
            int[] diziCategories;
            int[] diziAuthors;

            var dataCategories = Convert.ToInt32(form["categories"]);
            diziCategories = new int[] { dataCategories };

            var dataAuthors = Convert.ToInt32(form["authors"]);
            diziAuthors = new int[] { dataAuthors };            



            var entity = new News() 
            {
                Title=model.Title,
                Content=model.Content,
                Description=model.Description,
                Date=model.Date,
                Url=model.Url,
                IsApproved=model.IsApproved
            };

            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                //ViewBag.NCategories = _nCategoryServices.GetAll();
                //ViewBag.Authors = _authorServices.GetAll();

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _newsServices.NewsCreate(entity, diziCategories, diziAuthors);
            TempData.Put("message", new AlertMessage()
            {
                Title = "Yeni Haber Ekleme",
                Message = $"{model.Title} Haberi Başarıyla Eklendi",
                AlertType = "success"
            });
            return RedirectToAction("NewsListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult NewsEditAdmin(int? id)
        {   
            var entity = _newsServices.GetByWithCategoriesAndAuthorId((int) id);

            var model = new NewsModel()
            {
                NewsId=entity.NewsId,
                Title=entity.Title,
                Content=entity.Content,
                Description=entity.Description,
                Date=entity.Date,
                Image=entity.Image,
                Url=entity.Url,
                IsApproved=entity.IsApproved,
                nCategories= entity.newsHCategories.Select(i => i.NCategories).ToList(),
                Authors = entity.newsAuthors.Select(i => i.Author).ToList(),
                NCategoryId=entity.newsHCategories.Select(i=>i.NCategoryId).FirstOrDefault(),
               AuthorId=entity.newsAuthors.Select(i=>i.AuthorId).FirstOrDefault()
            };
            ViewBag.NCategories = _nCategoryServices.GetAll();
            ViewBag.Authors = _authorServices.GetAll();
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> NewsEditAdmin(NewsModel model, IFormFile file, IFormCollection form)
        {
            int[] diziCategories;
            int[] diziAuthors;

            var dataCategories = Convert.ToInt32(form["categories"]);
            diziCategories = new int[] { dataCategories };

            var dataAuthors = Convert.ToInt32(form["authors"]);
            diziAuthors = new int[] { dataAuthors };

            var entity = _newsServices.GetByWithCategoriesAndAuthorId(model.NewsId);
            entity.Title = model.Title;
            entity.Content = model.Content;
            entity.Description = model.Description;
            entity.Date = model.Date;
            entity.Url = model.Url;
            entity.IsApproved = model.IsApproved;

            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _newsServices.Update(entity, diziCategories, diziAuthors);
            TempData.Put("message", new AlertMessage()
            {
                Title = "Haber Güncelleme",
                Message = $"{model.Title} Haberi Başarıyla Güncellendi",
                AlertType = "warning"
            });
            return RedirectToAction("NewsListAdmin");
        }
        [Authorize(Roles = "admin")]
        public IActionResult NewsDeleteAdmin(int NewsId)   //authorId list.cshtmldeki alandada yazıyor ben buraya id diyodum ve silmiyodu sonra anladımki cshtmldeki name alanına ne yzarsam burayada onu yazmalıyım  
        {
            var entity = _newsServices.GetById(NewsId);
            var name = entity.Title;
            _newsServices.Delete(entity);
            TempData.Put("message", new AlertMessage()
            {
                Title = "Haber Silme",
                Message = $"{name} Haberi Başarıyla Silindi",
                AlertType = "danger"
            });
            return RedirectToAction("NewsListAdmin");
        }
        //---------- HaberKategori 
        [Authorize(Roles = "admin")]
        public IActionResult NewsCategoryListAdmin()    
        {
            return View(new NCategoryListViewModel
            {
                nCategories = _nCategoryServices.GetAll()
            });
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult NewsCategoryCreateAdmin()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> NewsCategoryCreateAdmin(NCategoryModel model,IFormFile file)
        {
            var entity = new NCategory()
            {
                Tittle = model.Tittle,
                Url = model.Url,
            };
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _nCategoryServices.Create(entity);
            var name = entity.Tittle;
            TempData.Put("message", new AlertMessage()
            {
                Title = "Kategori Ekleme",
                Message = $"{name} Kategorisi Başarıyla Eklendi",
                AlertType = "success"
            });
            return RedirectToAction("NewsCategoryListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult NewsCategoryEditAdmin(int id)
        {
            var entity = _nCategoryServices.GetById(id);
            var model = new NCategoryModel()
            {
                NCategoryId = entity.NCategoryId,
                Tittle = entity.Tittle,
                Image = entity.Image,
                Url = entity.Url
            };
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> NewsCategoryEditAdmin(NCategoryModel model,IFormFile file) 
        {
            var entity = _nCategoryServices.GetById(model.NCategoryId);
            entity.Tittle = model.Tittle;
            entity.Url = model.Url;

            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _nCategoryServices.Update(entity);
            var name = entity.Tittle;
            TempData.Put("message", new AlertMessage()
            {
                Title = "Kategori Güncelleme",
                Message = $"{name} Kategorisi Başarıyla Güncellendi",
                AlertType = "warning"
            });
            return RedirectToAction("NewsCategoryListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult NewsCategoryDeleteAdmin(int NCategoryId)  
        {
            var entity = _nCategoryServices.GetById(NCategoryId);
            var name = entity.Tittle;
            _nCategoryServices.Delete(entity);
            TempData.Put("message", new AlertMessage()
            {
                Title = "Kategori Silme",
                Message = $"{name} Kategorisi Başarıyla Silindi",
                AlertType = "danger"
            });
            return RedirectToAction("NewsCategoryListAdmin");
        }
        //------------------ Video CRUD
        [Authorize(Roles = "admin")]
        public IActionResult VideoListAdmin()
        {
            return View(new VideoListViewModel { Videos=_videoServices.GetAll()});
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult VideoCreateAdmin()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> VideoCreateAdmin(VideoModel model,IFormFile file)
        {
            var entity = new Video()        
            {
                Tittle=model.Tittle,        
                Url=model.Url,
                Date=model.Date
            };
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _videoServices.Create(entity);
            var name = entity.Tittle;
            TempData.Put("message", new AlertMessage()
            {
                Title = "Video Ekleme",
                Message = $"{name} Video Başarıyla Eklendi",
                AlertType = "success"
            });
            return RedirectToAction("VideoListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult VideoEditAdmin(int id)
        {
            var entity = _videoServices.GetById(id);
            var model = new VideoModel()
            {
                VideoId=entity.VideoId,
                Tittle=entity.Tittle,
                Url=entity.Url,
                Date=entity.Date
            };
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult VideoEditAdmin(VideoModel model)
        {
            var entity = _videoServices.GetById(model.VideoId);
            entity.Tittle = model.Tittle;
            entity.Url = model.Url;
            entity.Date = model.Date;

            _videoServices.Update(entity);

            var name = entity.Tittle;
            TempData.Put("message", new AlertMessage()
            {
                Title = "Video Güncelleme",
                Message = $"{name} Video Başarıyla Güncellendi",
                AlertType = "warning"
            });
            return RedirectToAction("VideoListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult VideoDeleteAdmin(int VideoId)
        {
            var entity = _videoServices.GetById(VideoId);
            _videoServices.Delete(entity);
            var name = entity.Tittle;
            TempData.Put("message", new AlertMessage()
            {
                Title = "Video Silme",
                Message = $"{name} Video Başarıyla Silindi",
                AlertType = "danger"
            });
            return RedirectToAction("VideoListAdmin");
        }
        [Authorize(Roles = "admin")]
        // SLİDER CRUD-------------------------------------------------------
        public IActionResult SliderListAdmin()
        {
            return View(new SliderListViewModel { Sliders = _sliderServices.GetOrderAll() });
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult SliderCreateAdmin()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> SliderCreateAdmin(SliderModel model,IFormFile file)
        {
            var entity = new Slider()
            {
                 Tittle=model.Tittle,
                 Description=model.Description,
                 Url=model.Url
            };                          
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _sliderServices.Create(entity);
            var name = entity.Tittle;
            TempData.Put("message", new AlertMessage()
            {
                Title = "Slider Ekleme",
                Message = $"{name} Slider Başarıyla Eklendi",
                AlertType = "success"
            });
            return RedirectToAction("SliderListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult SliderEditAdmin(int id)
        {
            var entity = _sliderServices.GetById(id);
            var model = new SliderModel()
            {
                SliderId=entity.SliderId,
                Tittle=entity.Tittle,
                Description=entity.Description,
                Image=entity.Image,
                Url=entity.Url
            };
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost]      
        public async Task<IActionResult> SliderEditAdmin(SliderModel model, IFormFile file)
        {
            var entity = _sliderServices.GetById(model.SliderId);
            entity.Tittle = model.Tittle;
            entity.Description = model.Description;
            entity.Image = model.Image;
            entity.Url = model.Url;
            var name = entity.Tittle;

            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            _sliderServices.Update(entity);

            TempData.Put("message", new AlertMessage()
            {
                Title = "Slider Güncelleme",
                Message = $"{name} Slider Başarıyla Güncellendi",
                AlertType = "warning"
            });

            return RedirectToAction("SliderListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult SliderDeleteAdmin(int SliderId)
        {
            var entity = _sliderServices.GetById(SliderId);
            var name = entity.Tittle;
            _sliderServices.Delete(entity);
            TempData.Put("message", new AlertMessage()
            {
                Title = "Slider Silme",
                Message = $"{name} Slider Başarıyla Silindi",
                AlertType = "danger"
            });
            return RedirectToAction("SliderListAdmin");
        }
        //---------- Etkinlikler Activities
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ActivitiesListAdmin()
        {
            return View(new ActivitiesListViewModel { activities=_activitiesServices.GetOrderAll()});
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ActivitiesCreateAdmin()    
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> ActivitiesCreateAdmin(ActivitiesModel model,IFormFile file)
        {
            var entity = new Activities()
            {
                Tittle=model.Tittle,
                Description=model.Description,
                Content=model.Content,
                Date=model.Date,
                Url=model.Url
            };
            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _activitiesServices.Create(entity);
            var name = entity.Tittle;
            TempData.Put("message", new AlertMessage()
            {
                Title = "Etkinlik Ekleme",
                Message = $"{name} Etkinlik Başarıyla Eklendi",
                AlertType = "success"
            });
            return RedirectToAction("ActivitiesListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult ActivitiesEditAdmin(int? id)
        {
            var entity = _activitiesServices.GetById((int)id);
            var model = new ActivitiesModel()
            {
                ActivitiesId = entity.ActivitiesId,
                Tittle = entity.Tittle,
                Description = entity.Description,
                Content = entity.Content,
                Date = entity.Date,
                Image = entity.Image,
                Url = entity.Url
            };
            return View(model);                 
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> ActivitiesEditAdmin(ActivitiesModel model, IFormFile file)
        {
            var entity = _activitiesServices.GetById(model.ActivitiesId);
            entity.Tittle = model.Tittle;
            entity.Description = model.Description;
            entity.Content = model.Content;
            entity.Date = model.Date;
            entity.Image = model.Image;
            entity.Url = model.Url;

            if (file != null)
            {
                var extention = Path.GetExtension(file.FileName);
                var randomName = string.Format($"{Guid.NewGuid()}{extention}");
                entity.Image = randomName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", randomName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            _activitiesServices.Update(entity);
            var name = entity.Tittle;
            TempData.Put("message", new AlertMessage()
            {
                Title = "Etkinlik Güncelleme",
                Message = $"{name} Etkinlik Başarıyla Güncelleme",
                AlertType = "warning"
            });
            return RedirectToAction("ActivitiesListAdmin");
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult ActivitiesDeleteAdmin(int ActivityId)
        {
            var entity = _activitiesServices.GetById(ActivityId);
            _activitiesServices.Delete(entity);
            var name = entity.Tittle;
            TempData.Put("message", new AlertMessage()
            {
                Title = "Etkinlik Silme",
                Message = $"{name} Etkinlik Başarıyla Silindi",
                AlertType = "danger"
            });
            return RedirectToAction("ActivitiesListAdmin");
        }

        [HttpGet]
        public IActionResult AdminPanel()
        {
            return View();
        }
        //-------- EMAİL Abone ol gönderme işlemi
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Email(EMailModel m)  //Mail sınıfından m diye bir değişken tanımlarız
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Burası aynı kalacak
                client.Credentials = new NetworkCredential("ahmetikrdg@gmail.com", "153248679");//<<<<<<<<<<<<<<<<<<<
                client.EnableSsl = true;
                MailMessage msj = new MailMessage(); //Yeni bir MailMesajı oluşturuyoruz
                msj.From = new MailAddress(m.Email, m.NameSurname); //iletişim kısmında girilecek mail buaraya gelecektir
                msj.To.Add("ahmetikrdg@gmail.com"); //Buraya kendi mail adresimizi yazıyoruz <<<<<
                //Bu kısımdan itibaren sizden kullanıcıya gidecek mail bilgisidir 
                MailMessage msj1 = new MailMessage();
                msj1.From = new MailAddress("ahmetikrdg@gmail.com", "Haliç TeknolojiPortalı");//<<<<<<<<<<<<<<<<<<<<<<<<<<
                msj1.To.Add(m.Email); //Buraua iletişim sayfasında gelecek mail adresi gelecktir.
                msj1.Subject = "Haliç Hub Aboneliği";
                msj1.Body = "Haliç Hub Teknoloji Portalına Aboneliğiniz Gerçekleşmiştir. Aramıza Hoşgeldiniz...";
                client.Send(msj1);
                //aslında foreachla modelin içindekileri dolaşıp mesajı gönderebiliriz
                var entity = new EMail()
                {
                    NameSurname=m.NameSurname,
                    Email = m.Email
                };
                _emailServices.Create(entity);
                ViewBag.Succes = "teşekkürler Mailniz başarı bir şekilde gönderildi"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
                if (TempData["Url"]==null)
                {
                    return Redirect("/");
                }
                return Redirect("/makaleler/" + TempData["Url"]);
            }
            catch (Exception)
            {
                ViewBag.Error = "Mesaj Gönderilken hata olıuştu"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
                return Redirect("/Halic/makaleler");
            }
        }

        [AllowAnonymous]
        [HttpPost]      
        public IActionResult NewsEmail(EMailModel m)  //Mail sınıfından m diye bir değişken tanımlarız
        {
            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Burası aynı kalacak
                client.Credentials = new NetworkCredential("ahmetikrdg@gmail.com", "153248679");
                client.EnableSsl = true;
                MailMessage msj = new MailMessage(); //Yeni bir MailMesajı oluşturuyoruz
                msj.From = new MailAddress(m.Email, m.NameSurname); //iletişim kısmında girilecek mail buaraya gelecektir
                msj.To.Add("ahmetikrdg@gmail.com"); //Buraya kendi mail adresimizi yazıyoruz<<<<<<<<<<<<<<<<<<
                //Bu kısımdan itibaren sizden kullanıcıya gidecek mail bilgisidir 
                MailMessage msj1 = new MailMessage();
                msj1.From = new MailAddress("ahmetikrdg@gmail.com", "Haliç Teknoloji Portalı");//<<<<<<<<<<<
                msj1.To.Add(m.Email); //Buraua iletişim sayfasında gelecek mail adresi gelecktir.
                msj1.Subject = "Haliç Hub Aboneliği";
                msj1.Body = "Haliç Hub Teknoloji Portalına Aboneliğiniz Gerçekleşmiştir. Aramıza Hoşgeldiniz...";
                client.Send(msj1);
                //aslında foreachla modelin içindekileri dolaşıp mesajı gönderebiliriz
                var entity = new EMail()
                {
                    NameSurname=m.NameSurname,
                    Email = m.Email
                };
                _emailServices.Create(entity);
                ViewBag.Succes = "teşekkürler Mailniz başarı bir şekilde gönderildi"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
                return Redirect("/Haberler/" + TempData["Nurl"]);

            }
            catch (Exception)
            {
                ViewBag.Error = "Mesaj Gönderilken hata olıuştu"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
                return Redirect("/Halic/makaleler");
            }
        }

        //-- EMAİL CRUD İŞLEMLERİ
        [Authorize(Roles = "admin")]
        public IActionResult EMailList()
        {
            return View(new EMailListViewModel() { EMails=_emailServices.GetAll()});
        }
        [Authorize(Roles = "admin")]
        [HttpGet]
        public IActionResult EMailSender()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EMailSender(EMailListViewModel model,EMailModel m)
        { var s = m.Subject;
            var b = m.Body;
            var tut = _emailServices.GetAll();
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Burası aynı kalacak
            client.Credentials = new NetworkCredential("ahmetikrdg@gmail.com", "153248679");//<<<<<<<<<<<<<
            client.EnableSsl = true;
            MailMessage msj = new MailMessage(); //Yeni bir MailMesajı oluşturuyoruz
            foreach (var item in tut)       
            {//hata veriyosa güncellemedeki gibi yapsam
                msj.From = new MailAddress(item.Email, item.NameSurname); //iletişim kısmında girilecek mail buaraya gelecektir
                msj.To.Add("ahmetikrdg@gmail.com");// <<<<<<<<<<<<<<<<<<<<<<< 
                MailMessage msj1 = new MailMessage();
                msj1.From = new MailAddress("ahmetikrdg@gmail.com", "Haliç Teknoloji Portalı"); //<<<<<<<<<<<<<<<<<
                msj1.To.Add(item.Email); //Buraya iletişim sayfasında gelecek mail adresi gelecktir.
                msj1.Subject = s;
                msj1.Body = b;
                client.Send(msj1);
            }
           
          
            ViewBag.Succes = "teşekkürler Mailniz başarı bir şekilde gönderildi"; //Bu kısımlarda ise kullanıcıya bilgi vermek amacı ile olur
            return Redirect("/Admin/EMailList");
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult EMailDelete(int id)
        {
            var emailaccount = _emailServices.GetById(id);
            _emailServices.Delete(emailaccount);
            return Redirect("/Admin/EMailList");
        }

       
    }
}
