using Halic.Bussiness.Abstract;
using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Concrate
{
    public class ArticleManager : IArticleServices
    {
        private IArticleRepository _articleRepository;
        public ArticleManager(IArticleRepository articlerepository)
        {
            _articleRepository = articlerepository;
        }

        public Article ArticleCreate(Article article, int[] categories, int[] authors)
        {
           return _articleRepository.ArticleCreate(article, categories,authors);
        }

        public void Create(Article Entity)
        {
            _articleRepository.Create(Entity);
        }

        public void Delete(Article Entity)
        {
            _articleRepository.Delete(Entity);
        }

        public List<Article> GetAll()
        {
            return _articleRepository.GetAll();
        }

        public List<Article> GetAllToTake()
        {
            return _articleRepository.GetAllToTake();
        }

        public List<Article> GetArticleByCategory(string name, int page, int pageSize)
        {
            return _articleRepository.GetArticleByCategory(name,page,pageSize);
        }

        public object GetArticleCategoryDetails(Category t)
        {
            return _articleRepository.GetArticleCategoryDetails(t);
        }

        public Article GetArticleDetails(string url)
        {
            return _articleRepository.GetArticleDetails(url);
        }

        public Article GetById(int id)
        {
            return _articleRepository.GetById(id);
        }

        public Article GetByWithCategoriesId(int id)
        {
            return _articleRepository.GetByWithCategoriesId(id);
        }

        public int GetCountByCategory(string category)
        {
            return _articleRepository.GetCountByCategory(category);
        }

        public List<Article> GetOrderAll()
        {
            return _articleRepository.GetOrderAll();
        }

        public List<Article> GetSearchResult(string search)
        {       
            return _articleRepository.GetSearchResult(search);
        }

        public List<Article> IsApproved()
        {
            return _articleRepository.IsApproved();
        }

        public void Update(Article Entity)
        {
            _articleRepository.Update(Entity);
        }

        public void Update(Article entity, int[] categoryIds, int[] authors)
        {
            _articleRepository.Update(entity, categoryIds,authors);
        }

        
    }
}
