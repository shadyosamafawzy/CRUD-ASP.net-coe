using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.Models;
using Bookstore.Models.Repositories;
using Bookstore.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookstore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookstoreRepository<Book> bookRepository;
        private readonly IBookstoreRepository<Author> authorRepository;
#pragma warning disable CS0618 // Type or member is obsolete
        private readonly IHostingEnvironment hosting;
#pragma warning restore CS0618 // Type or member is obsolete

        public BookController(IBookstoreRepository<Book> bookRepository,
            IBookstoreRepository<Author> authorRepository,
#pragma warning disable CS0618 // Type or member is obsolete
            IHostingEnvironment hosting)
#pragma warning restore CS0618 // Type or member is obsolete
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.hosting = hosting;
        }

        // GET: Book
        public ActionResult Index()
        {
            var books = bookRepository.List();
            return View(books);
        }

        // GET: Book/Details/5
        public ActionResult Details(int id)
        {
            var book = bookRepository.find(id);
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel {

                authors = fillSelectList()
            };

            return View(model);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel bookAuthorViewModel)
        {
           

            if (ModelState.IsValid)
            {

                try
                {
                    string fileName = imagePath(bookAuthorViewModel.File) ?? string.Empty;



                    if (bookAuthorViewModel.authorId == -1)
                    {
                        ViewBag.message = "please select an author from the list";


                        return View(getAllAuthors());
                    }
                    Book book = new Book
                    {
                        id = bookAuthorViewModel.bookId,
                        title = bookAuthorViewModel.title,
                        description = bookAuthorViewModel.description,
                        imageUrl = fileName,
                        author = authorRepository.find(bookAuthorViewModel.authorId)
                    };

                    bookRepository.add(book);

                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
            return View(getAllAuthors());
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int id)
        {


            var book = bookRepository.find(id);
            var author = book.author == null ? book.author.id = 0 : book.author.id;
            var modelView = new BookAuthorViewModel
            {
                bookId = book.id,
                title = book.title,
                description = book.description,
                authorId = author,
                imageUrl = book.imageUrl,
                authors = authorRepository.List().ToList()

            };
            return View(modelView);
        }

        // POST: Book/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel bookAuthorViewModel)
        {
            try
            {
                string fileName = uploadImage(bookAuthorViewModel.File,bookAuthorViewModel.imageUrl);

               

                Book book = new Book
                {
                    id = bookAuthorViewModel.bookId,
                    title = bookAuthorViewModel.title,
                    description = bookAuthorViewModel.description,
                    author = authorRepository.find(bookAuthorViewModel.authorId),
                    imageUrl= fileName
                    
                };

                bookRepository.update(bookAuthorViewModel.bookId,book);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                return View();
            }
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int id)
        {
            var book = bookRepository.find(id);
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult confirmDelete(int id)
        {
            try
            {
                bookRepository.delete(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        List<Author> fillSelectList()
        {
            var authors = authorRepository.List().ToList();
            authors.Insert(0, new Author { id= -1 , fullName = "---Please Select an author"});
            return authors;
        }

        BookAuthorViewModel getAllAuthors()
        {
            var vModel = new BookAuthorViewModel
            {

                authors = fillSelectList()
            };
            return vModel;
        }

        string imagePath(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(hosting.WebRootPath, "images");
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));
                return file.FileName;
            }
            return null;
        }

        string uploadImage(IFormFile file,string imageUrl)
        {
            if (file != null)
            {

                
                string uploads = Path.Combine(hosting.WebRootPath, "images");
                string newPath = Path.Combine(uploads, file.FileName);


                //check if no image exiest before , save it directly
                if (imageUrl == null)
                {
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                    return file.FileName;

                }      
                else
                {
                    string oldPath = Path.Combine(uploads, imageUrl);
                    if (newPath != oldPath)
                    {
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                        file.CopyTo(new FileStream(newPath, FileMode.Create));

                    }
                    return file.FileName;

                }

            }




            return imageUrl;
        }


        public ActionResult search(string term)
        {
            var result = bookRepository.search(term);
            return View("Index",result);
        }
    }
}