using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Bussiness.Abstract
{
    public interface IAuthorServices
    {
        Author GetById(int id);
        List<Author> GetAll();
        void Create(Author Entity);
        void Update(Author Entity);
        void Delete(Author Entity);
        Author GetAuthorDetails(string url);
        List<Author> GetOrderAll();
        List<Author> GetSearchResult(string search);



    }
}
