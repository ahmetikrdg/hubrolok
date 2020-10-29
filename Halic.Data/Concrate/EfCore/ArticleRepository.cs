using Halic.Data.Abstract;
using Halic.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halic.Data.Concrate.EfCore
{
    public class ArticleRepository : GenericRepository<Article, HalicContext>, IArticleRepository
    {

        public List<Article> GetArticleByCategory(string url, int page, int pageSize)
        {
            using (var context = new HalicContext())
            {
                var articles = context.Articles.OrderByDescending(i=>i.ArticleId).Where(i => i.IsApproved == true).AsQueryable();
                if(!string.IsNullOrEmpty(url))
                {
                    articles = articles
                        .Include(i => i.ArticleCategories)
                        .ThenInclude(i => i.Categories)
                        .Where(i=>i.ArticleCategories.Any(a=>a.Categories.Url== url));
                }
                return articles.Skip((page-1)*pageSize).Take(pageSize).ToList();
            }
        }

        public Article GetArticleDetails(string url)
        {
            using (var context = new HalicContext())
            {
                return context.Articles
                .Where(i => i.Url == url)
                .Include(i=>i.ArticleAuthors)
                .ThenInclude(i => i.Authors)
                .Include(i=>i.ArticleCategories)
                .ThenInclude(i=>i.Categories)
                .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
             using (var context = new HalicContext())
             {
                 var article = context.Articles.AsQueryable();//asquarible biz sorguyu yazıyoruz ama vtye göndermeden önce üzerine ekstra link kriter belirlemek istiyorum demek
                 if (!string.IsNullOrEmpty(category))//kategori boş değilse kategoriye göre filitrele
                 {
                     article = article//ürün bilgilerinin
                    .Include(i => i.ArticleCategories)//productcategorislerini
                    .ThenInclude(i => i.Categories)//sonra kategorilerini yüklüyoruz.Daha sonra şart ekleyeceğiz şart en son çünkü ilgili kayıtların referanslarına ulaşmam lazım
                    .Where(i => i.ArticleCategories.Any(a => a.Categories.Url == category));//ilgili kaydın productcategorislerine gidiyoruz kategorilerine geçiyoruz ve gönderdiğimiz kategoriye ait bir ürün varsa any bana true döndürür oda o ürünü bana getir demek 
                 }
                 return article.Count();
             }
        }

        public List<Article> GetSearchResult(string search)
        {
            using (var context = new HalicContext())
            {
                var article = context.Articles.Where(i => i.IsApproved == true &&
                 i.Title.ToLower().Contains(search.ToLower()) ||
                 i.Description.ToLower().Contains(search.ToLower()) ||
                 i.Content.ToLower().Contains(search.ToLower()))
                 .AsQueryable();

                return article.ToList();
            }       
        }

        public List<Article> IsApproved()
        {
            using (var context = new HalicContext())
            {
                return context.Articles.Where(i => i.IsApproved == true).ToList();
            }    
        }

        public List<Article> GetAllToTake()
        {
            using (var context = new HalicContext())
            {
                return context.Articles.Where(i=>i.IsApproved).OrderByDescending(i=>i.ArticleId).Take(3).ToList();
            }
        }

        public Article GetByWithCategoriesId(int id)
        {
            using (var context = new HalicContext())
            {
                return context.Articles.Where(i => i.ArticleId == id)
                    .Include(i => i.ArticleCategories)
                    .ThenInclude(i => i.Categories)
                    .Include(i=>i.ArticleAuthors).ThenInclude(i=>i.Authors)
                    .FirstOrDefault();
            }
        }

        public void Update(Article entity, int[] categoryIds, int[] authors)
        {
            using (var context = new HalicContext())
            {
                
                var article = context.Articles
                    .Include(i=>i.ArticleCategories)
                    .Include(i=>i.ArticleAuthors)
                    .FirstOrDefault(i => i.ArticleId == entity.ArticleId);
                if(article!=null)
                {
                    article.Title = entity.Title;
                    article.Content = entity.Content;
                    article.Description = entity.Description;
                    article.Date = entity.Date;
                    article.Image = entity.Image;
                    article.Url = entity.Url;
                    article.IsApproved = entity.IsApproved;
                    article.ArticleCategories = categoryIds.Select(i => new ArticleCategory
                    {
                        ArticleId=entity.ArticleId,
                        CategoryId=i
                    }).ToList();
                    article.ArticleAuthors = authors.Select(i => new ArticleAuthor
                    {
                        ArticleId=entity.ArticleId,
                        AuthorId=i
                    }).ToList();
                    context.SaveChanges();
                }
            }

        }
        public Article ArticleCreate(Article article,int[] categories, int[] authors)    
        {
            using (var context = new HalicContext())
            {
                Article _article = new Article();
                _article = article;

          
                    _article.ArticleCategories = categories.Select(i => new ArticleCategory
                    {
                        ArticleId = _article.ArticleId,
                        CategoryId = i
                    }).ToList();

                _article.ArticleAuthors = authors.Select(i => new ArticleAuthor
                {
                    ArticleId = _article.ArticleId,
                    AuthorId = i
                }).ToList();
                

                context.Articles.Add(_article);
                context.SaveChanges();

                return _article;

            }
        }

        public List<Article> GetOrderAll()
        {
            using(var context=new HalicContext())
            {
                return context.Articles.OrderByDescending(i => i.ArticleId).ToList();
            }
        }

        public Article GetArticleCategoryDetails(string categories)
        {
            using (var context = new HalicContext())
            {
                var     articles =context.Articles
                        .Include(i => i.ArticleCategories)
                        .ThenInclude(i => i.Categories)
                        .Where(i => i.ArticleCategories.Any(a => a.Categories.Url == categories));
                
                return articles.Take(3).FirstOrDefault();
            }
        }

        public object GetArticleCategoryDetails(Category t)
        {
            string k = t.Url.ToString();
            using (var context = new HalicContext())
            {
                var articles = context.Articles
                        .Include(i => i.ArticleCategories)
                        .ThenInclude(i => i.Categories)
                        .Where(i => i.ArticleCategories.Any(a => a.Categories.Url == k)).Include(i=>i.ArticleCategories).ThenInclude(i=>i.Articles);

                return articles.OrderByDescending(i=>i.ArticleId).Take(4).ToList();
            }
        }
    }
}
