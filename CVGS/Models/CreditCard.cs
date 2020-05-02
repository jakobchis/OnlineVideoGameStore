using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.CustomValidation;

namespace CVGS.Models
{
    public class CreditCard
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [CreditCardValidation(ErrorMessage = "Invalid Credit Card Entered")]
        public long Number { get; set; }
        [DataType(DataType.Date)]
        public DateTime Expiry { get; set; }
        public string Code { get; set; }
        [Display(Name = "Associated Address")]
        public Guid AddressId { get; set; }
        public Guid UserId { get; set; }

        public Address Address { get; set; }
        public ApplicationUser User { get; set; }
    }
}
