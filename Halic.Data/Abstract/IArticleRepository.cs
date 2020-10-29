using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace Halic.Data.Abstract
{
    public interface IArticleRepository:IRepository<Article>
    {
        Article GetArticleDetails(string url);
        List<Article> GetArticleByCategory(string name,int page,int pageSize);
        int GetCountByCategory(string category);
        List<Article> GetSearchResult(string search);
        List<Article> IsApproved();
        List<Article> GetAllToTake();
        Article GetByWithCategoriesId(int id);
        void Update(Article entity, int[] categoryIds, int[] authors);
        Article ArticleCreate(Article article, int[] categories, int[] authors);
        List<Article> GetOrderAll();
        Article GetArticleCategoryDetails(string categories);
        object GetArticleCategoryDetails(Category t);
    }
}
