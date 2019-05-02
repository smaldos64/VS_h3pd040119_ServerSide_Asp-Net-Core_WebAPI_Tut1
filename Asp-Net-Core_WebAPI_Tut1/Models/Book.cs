using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp_Net_Core_WebAPI_Tut1.Models
{
    public class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public long BookID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        public long BookCategoryID { get; set; }
        public virtual BookCategory BookCategory { get; set; }

        public long PublisherID { get; set; }
        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
