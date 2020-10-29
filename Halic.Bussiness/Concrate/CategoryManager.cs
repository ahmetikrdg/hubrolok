using Halic.Bussiness.Abstract;
using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Concrate
{
    public class CategoryManager : ICategoryServices
    {
        private ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryrepository)
        {
            _categoryRepository = categoryrepository;
        }
        public void Create(Category Entity)
        {
            _categoryRepository.Create(Entity);
        }

        public void Delete(Category Entity)
        {
            _categoryRepository.Delete(Entity);
        }

        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int id)
        {
           return _categoryRepository.GetById(id);
        }

        public List<Category> GetPopularCategory()
        {
            return _categoryRepository.GetPopularCategory();
        }

        public void Update(Category Entity)
        {
            _categoryRepository.Update(Entity);
        }
    }
}
