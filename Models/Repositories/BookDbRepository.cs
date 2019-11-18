using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class BookDbRepository : IBookstoreRepository<Book>
    {
        BookstoreDBContext db;

        public BookDbRepository(BookstoreDBContext _db)
        {
            db = _db;
        }
        public void add(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
        }

        public void delete(int id)
        {
            var book = find(id);
            db.Books.Remove(book);
            db.SaveChanges();
        }

        public Book find(int id)
        {
            var book = db.Books.Include(a => a.author).SingleOrDefault(b => b.id == id);
            return book;
        }

        public IList<Book> List()
        {
            return db.Books.Include(a=>a.author).ToList();
        }

        public void update(int id, Book newBook)
        {
            db.Books.Update(newBook);
            db.SaveChanges();
        }


        public List<Book> search(string term)
        {
            var result = db.Books.Include(a => a.author)
                .Where(b=>b.title.Contains(term)
                || b.description.Contains(term)
                || b.author.fullName.Contains(term))
                .ToList();
            return result;
        }
    }
}
