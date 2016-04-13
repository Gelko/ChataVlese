using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace chataVlese.Models
{
    public class Feedback
    {
        [Required()]
        public Int32 FeedbackID { get; set; }
        
        [Required(ErrorMessage = "Zadajte komentár!")]
        [Display(Name = "Komentár")]
        public String Comment { get; set; }
        [Display(Name = "Meno")]
        [Required(ErrorMessage = "Zadajte meno!")]
        public String Name { get; set; }
        [Range(1, 5)]
        [Required(ErrorMessage = "Zadajte hodnotenie od 1 do 5")]
        public Int32 Rating { get; set; }
        [Required()]
        public Int32 Is_Valid { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date_In { get; set; }
    }
}