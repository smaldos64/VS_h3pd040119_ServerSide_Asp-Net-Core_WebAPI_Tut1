using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp_Net_Core_WebAPI_Tut1.Models
{
    public class BookAuthor
    {
        [Key]
        public long BookID { get; set; }

        [Key]
        public long AuthorID { get; set; }

        public virtual Author Author { get; set; }

        public virtual Book Book { get; set; }
    }
}
