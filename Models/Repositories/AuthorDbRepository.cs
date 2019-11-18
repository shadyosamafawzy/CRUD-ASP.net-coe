using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class AuthorDbRepository : IBookstoreRepository<Author>
    {
        BookstoreDBContext db;
        public AuthorDbRepository(BookstoreDBContext _db)
        {
            db = _db;
        }
        public void add(Author author)
        {
            db.Authors.Add(author);
            db.SaveChanges();

        }

        public void delete(int id)
        {
            var author = find(id);
            db.Authors.Remove(author);
            db.SaveChanges();

        }

        public Author find(int id)
        {
            var author = db.Authors.SingleOrDefault(a => a.id == id);
            return author;
        }

        public IList<Author> List()
        {
            return db.Authors.ToList();
        }

        public List<Author> search(string term)
        {
            return db.Authors.Where(a => a.fullName.Contains(term)).ToList();
        }

        public void update(int id, Author newAuthor)
        {
            db.Authors.Update(newAuthor);
            db.SaveChanges();
        }
    }
}
