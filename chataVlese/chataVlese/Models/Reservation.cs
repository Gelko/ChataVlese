using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace chataVlese.Models
{
    public class Reservation
    {
        [Required()]
        public Int32 ReservationID { get; set; }

        [DataType(DataType.Date)]

        [Display(Name = "Dátum od")]
        [Required(ErrorMessage = "Zadajte dátum príchodu")]
        public DateTime DateFrom { get; set; }
        
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Zadajte dátum odchodu")]
        [Display(Name = "Dátum do")]
        public DateTime DateTo { get; set; }

        [Range(1, 15)]
        [Required(ErrorMessage = "Zadajte počet osôb od 1 do 15")]
        [Display(Name = "Počet osôb (1 - 15)")]
        public Int32 Persons { get; set; }

        [Display(Name = "Meno")]
        [Required(ErrorMessage = "Zadajte vaše meno")]
        public String FirstName {get;set;}

        [Required(ErrorMessage = "Zadajte vaše priezvisko")]
        [Display(Name = "Priezvisko")]
        public String LastName {get;set;}

        [Required(ErrorMessage = "Zadajte telefónne číslo")]
        [Display(Name = "Telefónne číslo")]
        public Int32 PhoneNr { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Zadajte emailovú adresu v správnom tvare")]
        [Display(Name = "Email")]
        public String Email { get; set; }

        [Display(Name = "Poznámka")]
        public String Comment { get; set; }

        public DateTime Date_In { get; set; }
    }
}