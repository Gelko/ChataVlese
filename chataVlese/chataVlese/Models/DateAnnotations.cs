using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace chataVlese.Models
{
    public class DateAnnotations : ValidationAttribute
    {

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public DateAnnotations(DateTime pDateFrom,DateTime pDateTo)
        {
            DateFrom = pDateFrom;
            DateTo   = pDateTo;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            return base.IsValid(value);
        }

    }
}