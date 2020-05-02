using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.CustomValidation
{
    public class DateOfBirthValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            const int MIN_AGE = 18;
            DateTime dateZero = new DateTime(1, 1, 1);
            DateTime dateOfBirth = Convert.ToDateTime(value);
            DateTime dateToday = DateTime.Today;
            TimeSpan timeSpan = dateToday - dateOfBirth;
            int age = (dateZero + timeSpan).Year - 1;
            if (age >= MIN_AGE)
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
