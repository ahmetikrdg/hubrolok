using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Abstract
{
    public interface INewsServices
    {
        News GetById(int id);
        List<News> GetAll();
        void Create(News Entity);
        void Update(News Entity);
        void Delete(News Entity);
        News GetNewsDetails(string url);
        List<News> GetNewsByCategory(string name, int page, int pageSize);
        int GetCountByCategory(string category);
        List<News> GetAllToTake();
        List<News> GetSearchResult(string search);
        News NewsCreate(News News, int[] ncategories, int[] authors);
        List<News> GetOrderAll();
        void Update(News entity, int[] categoryIds, int[] authors);
        News GetByWithCategoriesAndAuthorId(int id);
        object GetNewsCategoryDetails(NCategory N);

    }
}
