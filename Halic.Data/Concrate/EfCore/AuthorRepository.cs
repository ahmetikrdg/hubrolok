using Halic.Data.Abstract;
using Halic.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Halic.Data.Concrate.EfCore
{
    public class AuthorRepository : GenericRepository<Author, HalicContext>, IAuthorRepository
    {
        public Author GetAuthorDetails(string url)
        {
            using (var context = new HalicContext())
            {
                return context.Authors
                .Where(i=>i.Url== url)
                .Include(i => i.ArticleAuthors)
                .ThenInclude(i => i.Articles)
                .Include(i=>i.newsAuthors)
                .ThenInclude(i=>i.News)
                .FirstOrDefault();
            }
        }

        public List<Author> GetOrderAll()
        {
            using(var context=new HalicContext())
            {
                return context.Authors.OrderByDescending(i => i.AuthorId).ToList();
            }
        }

        public List<Author> GetSearchResult(string search)
        {
            using (var context = new HalicContext())
            {
                var author = context.Authors.Where(i =>
                 i.NameSurname.ToLower().Contains(search.ToLower())).AsQueryable();

                return author.ToList();
            }
        }
    }
}
