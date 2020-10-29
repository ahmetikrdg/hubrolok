using Halic.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Halic.Data.Abstract
{
    public interface IAuthorRepository:IRepository<Author>
    {
        Author GetAuthorDetails(string url);
        List<Author> GetOrderAll();
        List<Author> GetSearchResult(string search);

    }
}
