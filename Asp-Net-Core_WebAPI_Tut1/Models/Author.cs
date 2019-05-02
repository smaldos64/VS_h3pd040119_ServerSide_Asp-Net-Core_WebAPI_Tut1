using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp_Net_Core_WebAPI_Tut1.Models
{
    public class Author
    {
        public long AuthorID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string AuthorName { get; set; }

        //public long AuthorContactID { get; set; }
        public virtual AuthorContact AuthorContact { get; set; }

        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
