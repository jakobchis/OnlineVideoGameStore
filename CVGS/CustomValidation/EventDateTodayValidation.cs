using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.CustomValidation
{
    public class EventDateTodayValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dateEventPost = Convert.ToDateTime(value);
            DateTime dateToday = DateTime.Today;
            if (dateEventPost >= dateToday)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
