using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SunnyandKomal.Models
{
    public class GuestBook
    {
        public GuestBook()
        {

        }

        public int ID { get; set; }
        [Required]
        [DisplayName("Your Name")]
        public string Name { get; set; }
        public string Message { get; set; }
    }
}