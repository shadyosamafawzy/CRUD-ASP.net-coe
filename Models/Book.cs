using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.Models
{
    public class Book
    {
        public int id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string title { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 20)]
        public string description { get; set; }

        public string imageUrl { get; set; }
        public Author author { get; set; }

    }
}
