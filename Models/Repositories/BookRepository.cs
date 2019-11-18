using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models.Repositories
{
    public class BookRepository : IBookstoreRepository<Book>
    {

        List<Book> books;

        public BookRepository()
        {
            books = new List<Book>() { 
            
                new Book
                {
                    id=1,   title="C# programming", description = "no description" ,
                    author = new Author{ id = 2}
                },
                new Book
                {
                    id=2,   title="C++ programming", description = "no description",
                    author = new Author{ id = 1}

                },
                new Book
                {
                    id=3,   title="C programming", description = "no description",
                    author = new Author{ id = 3}

                }
            };
        }
        public void add(Book book)
        {
            book.id = books.Max(b => b.id) + 1;
            books.Add(book);
        }

        public void delete(int id)
        {
            var book = find(id);
            books.Remove(book);
        }

        public Book find(int id)
        {
            var book = books.SingleOrDefault(b => b.id == id);
            return book;
        }

        public IList<Book> List()
        {
            return books;
        }

        public List<Book> search(string term)
        {
            var result = books.Where(b => b.title.Contains(term)
                 || b.description.Contains(term)
                 || b.author.fullName.Contains(term))
                 .ToList();
            return result;
        }

        public void update(int id,Book newBook)
        {
            var book = find(id);
            book.title = newBook.title;
            book.description = newBook.description;
            book.author = newBook.author;
            book.imageUrl = newBook.imageUrl;
        }
    }
}
