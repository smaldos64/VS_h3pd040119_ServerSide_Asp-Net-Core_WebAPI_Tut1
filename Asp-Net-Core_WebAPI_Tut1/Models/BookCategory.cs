using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp_Net_Core_WebAPI_Tut1.Models
{
    public class BookCategory
    {
        public long BookCategoryID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string BookCategoryName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
