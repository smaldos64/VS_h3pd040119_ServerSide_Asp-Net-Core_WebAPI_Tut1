using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp_Net_Core_WebAPI_Tut1.Models
{
    public class Publisher
    {
        public long PublisherID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string PublisherName { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
