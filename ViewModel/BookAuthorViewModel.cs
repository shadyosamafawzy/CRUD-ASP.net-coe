using Bookstore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.ViewModel
{
    public class BookAuthorViewModel
    {
        public int bookId { get; set; }

        [Required]
        [StringLength(50,MinimumLength =1)]
        public string title { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 20)]
        public string description { get; set; }

        public IFormFile File { get; set; }

        public string imageUrl { get; set; }
        public int authorId { get; set; }
        public List<Author> authors { get; set; }

    }
}
