using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class AuthorRepository : IBookstoreRepository<Author>
    {
        List<Author> authors;
        public AuthorRepository()
        {
            authors = new List<Author>() {
                new Author{id = 1 , fullName = "shady osama"},
                new Author{id = 2 , fullName = "islam osama"},
                new Author{id = 3 , fullName = "osama osama"}
            };
        }
        public void add(Author author)
        {
            author.id = authors.Max(a => a.id) + 1;
            authors.Add(author);
        }

        public void delete(int id)
        {
            var author = find(id);
            authors.Remove(author);
        }

        public Author find(int id)
        {
            var author = authors.SingleOrDefault(a => a.id == id);
            return author;
        }

        public IList<Author> List()
        {
           return  authors;
        }

        public List<Author> search(string term)
        {
            return  authors.Where(a => a.fullName.Contains(term)).ToList();
        }

        public void update(int id, Author newAuthor)
        {
            var author = find(id);
            author.fullName = newAuthor.fullName;
        }
    }
}
