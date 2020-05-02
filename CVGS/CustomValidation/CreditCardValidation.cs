using CreditCardValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.CustomValidation
{
    public class CreditCardValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            CreditCardDetector detector = new CreditCardDetector(value.ToString());
            return detector.IsValid();
        }
    }
}
