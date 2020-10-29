using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Abstract
{
    public interface IArticleServices
    {
        Article GetById(int id);
        List<Article> GetAll();
        void Create(Article Entity);
        void Update(Article Entity);
        void Delete(Article Entity);
        Article GetArticleDetails(string url);
        List<Article> GetArticleByCategory(string name, int page, int pageSize);
        int GetCountByCategory(string category);
        List<Article> GetSearchResult(string search);
        List<Article> IsApproved();
        List<Article> GetAllToTake();
        Article GetByWithCategoriesId(int id);
        void Update(Article entity, int[] categoryIds, int[] authors);
        Article ArticleCreate(Article article, int[] categories, int[] authors);
        List<Article> GetOrderAll();
        object GetArticleCategoryDetails(Category t);
    }
}

