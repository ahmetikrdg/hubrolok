using Halic.Data.Abstract;
using Halic.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halic.Data.Concrate.EfCore
{
    public class NewsRepository : GenericRepository<News, HalicContext>, INewsRepository
    {
        public List<News> GetAllToTake()
        {
            using (var context = new HalicContext())
            {
                return context.News.OrderByDescending(i => i.NewsId).Take(5).ToList();
            }
        }

        public News GetByWithCategoriesAndAuthorId(int id)
        {
            using (var context = new HalicContext())
            {
                return context.News
                    .Include(i => i.newsAuthors)
                    .ThenInclude(i => i.Author)
                    .Include(i => i.newsHCategories)
                    .ThenInclude(i => i.NCategories).FirstOrDefault(i=>i.NewsId==id);
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new HalicContext())
            {
                var news = context.News.AsQueryable();//asquarible biz sorguyu yazıyoruz ama vtye göndermeden önce üzerine ekstra link kriter belirlemek istiyorum demek
                if (!string.IsNullOrEmpty(category))//kategori boş değilse kategoriye göre filitrele
                {
                    news = news//ürün bilgilerinin
                   .Include(i => i.newsHCategories)//productcategorislerini
                   .ThenInclude(i => i.NCategories)//sonra kategorilerini yüklüyoruz.Daha sonra şart ekleyeceğiz şart en son çünkü ilgili kayıtların referanslarına ulaşmam lazım
                   .Where(i => i.newsHCategories.Any(a => a.NCategories.Url == category));//ilgili kaydın productcategorislerine gidiyoruz kategorilerine geçiyoruz ve gönderdiğimiz kategoriye ait bir ürün varsa any bana true döndürür oda o ürünü bana getir demek 
                }
                return news.Count();
            }
        }

        public List<News> GetNewsByCategory(string name, int page, int pageSize)
        {
            using (var context = new HalicContext())
            {
                var news = context.News.OrderByDescending(i => i.NewsId).Where(i => i.IsApproved == true).AsQueryable();
                if (!string.IsNullOrEmpty(name))
                {
                    news = news
                        .Include(i => i.newsHCategories)
                        .ThenInclude(i => i.NCategories)
                        .Where(i => i.newsHCategories.Any(a => a.NCategories.Url == name));
                }
                return news.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public object GetNewsCategoryDetails(NCategory N)
        {
             string k = N.Url.ToString();    
             using (var context = new HalicContext())
             {
                 var newses = context.News
                         .Include(i => i.newsHCategories)
                         .ThenInclude(i => i.NCategories)
                         .Where(i => i.newsHCategories.Any(a => a.NCategories.Url == k)).Include(i => i.newsHCategories).ThenInclude(i => i.News);
                 return newses.OrderByDescending(i => i.NewsId).Take(4).ToList();
             }
        }

        public News GetNewsDetails(string url)
        {
            using (var context = new HalicContext())
            {
                return context.News
                .Where(i => i.Url == url)
                .Include(i => i.newsAuthors)
                .ThenInclude(i => i.Author)
                .Include(i => i.newsHCategories)
                .ThenInclude(i => i.NCategories)
                .FirstOrDefault();
            }
        }

        public List<News> GetOrderAll()
        {
            using (var context = new HalicContext())
            {
                return context.News.OrderByDescending(i => i.NewsId).ToList();
            }
        }

        public List<News> GetSearchResult(string search)
        {
            using (var context = new HalicContext())
            {
                var news = context.News.Where(i => i.IsApproved == true &&
                 i.Title.ToLower().Contains(search.ToLower()) ||
                 i.Description.ToLower().Contains(search.ToLower()) ||
                 i.Content.ToLower().Contains(search.ToLower())).AsQueryable();

                return news.ToList();
            }
        }

        public News NewsCreate(News News, int[] ncategories, int[] authors)    
        {
            using (var context = new HalicContext())
            {
                News _news = new News();
                _news = News;

                _news.newsAuthors = authors.Select(i => new NewsAuthor
                {
                    NewsId = _news.NewsId,
                    AuthorId = i
                }).ToList();

                _news.newsHCategories = ncategories.Select(i => new NewsHCategory { 
                    NCategoryId=i,
                    NewsId=_news.NewsId
                }).ToList();
                context.News.Add(_news);
                context.SaveChanges();
                return _news;
            }
        }

        public void Update(News entity, int[] categoryIds, int[] authors)       
        {
            using (var context = new HalicContext())
            {
                var News= context.News
                    .Include(i => i.newsAuthors)
                    .Include(i => i.newsHCategories)
                    .FirstOrDefault(i => i.NewsId == entity.NewsId);
                if(News != null)
                {
                    News.Title = entity.Title;
                    News.Content = entity.Content;
                    News.Description = entity.Description;
                    News.Date = entity.Date;
                    News.Image = entity.Image;
                    News.Url = entity.Url;
                    News.IsApproved = entity.IsApproved;

                    entity.newsHCategories = categoryIds.Select(i => new NewsHCategory
                    {
                        NewsId = entity.NewsId,
                        NCategoryId = i
                    }).ToList();
                    entity.newsAuthors = authors.Select(i => new NewsAuthor {
                        NewsId=entity.NewsId,
                        AuthorId=i
                    }).ToList();
                    context.SaveChanges();
                }     
            }
        }




    }
}
