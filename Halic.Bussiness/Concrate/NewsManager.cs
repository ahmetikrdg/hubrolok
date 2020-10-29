using Halic.Bussiness.Abstract;
using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Concrate
{
    public class NewsManager : INewsServices
    {
        private INewsRepository _newsRepository;
        public NewsManager(INewsRepository newsrepository)
        {
            _newsRepository = newsrepository;
        }

        public void Create(News Entity)
        {
            _newsRepository.Create(Entity);
        }

        public void Delete(News Entity)
        {
            _newsRepository.Delete(Entity);
        }

        public List<News> GetAll()
        {
            return _newsRepository.GetAll();
        }

        public List<News> GetAllToTake()
        {
            return _newsRepository.GetAllToTake();
        }

        public News GetById(int id)
        {
            return _newsRepository.GetById(id);
        }

        public News GetByWithCategoriesAndAuthorId(int id)
        {
            return _newsRepository.GetByWithCategoriesAndAuthorId(id);
        }

        public int GetCountByCategory(string category)
        {
            return _newsRepository.GetCountByCategory(category);
        }

        public List<News> GetNewsByCategory(string name, int page, int pageSize)
        {
            return _newsRepository.GetNewsByCategory(name, page, pageSize);
        }

        public object GetNewsCategoryDetails(NCategory N)
        {
            return _newsRepository.GetNewsCategoryDetails(N);
        }

        public News GetNewsDetails(string url)
        {
            return _newsRepository.GetNewsDetails(url);
        }

        public List<News> GetOrderAll()
        {
            return _newsRepository.GetOrderAll();
        }

        public List<News> GetSearchResult(string search)
        {
            return _newsRepository.GetSearchResult(search);
        }

        public News NewsCreate(News News, int[] ncategories, int[] authors)
        {
            return _newsRepository.NewsCreate(News,ncategories,authors);
        }

        public void Update(News Entity)
        {
            _newsRepository.Update(Entity);
        }

        public void Update(News entity, int[] categoryIds, int[] authors)
        {
            _newsRepository.Update(entity, categoryIds, authors);
        }
    }
}
