using Halic.Bussiness.Abstract;
using Halic.Data.Abstract;
using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Concrate
{
    public class AuthorManager : IAuthorServices
    {
        private IAuthorRepository _authorRepository;
        public AuthorManager(IAuthorRepository authorpository)
        {
            _authorRepository = authorpository;
        }
        public void Create(Author Entity)
        {
            _authorRepository.Create(Entity);
        }

        public void Delete(Author Entity)
        {
            _authorRepository.Delete(Entity);
        }

        public List<Author> GetAll()
        {
           return _authorRepository.GetAll();
        }

        public Author GetAuthorDetails(string url)
        {
            return _authorRepository.GetAuthorDetails(url);
        }

        public Author GetById(int id)
        {
            return _authorRepository.GetById(id);
        }

        public List<Author> GetOrderAll()
        {
            return _authorRepository.GetOrderAll();
        }

        public List<Author> GetSearchResult(string search)
        {
            return _authorRepository.GetSearchResult(search);
        }

        public void Update(Author Entity)
        {
            _authorRepository.Update(Entity);
        }
    }
}
