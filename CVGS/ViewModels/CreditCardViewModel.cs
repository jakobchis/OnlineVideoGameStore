using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Models;
using System.ComponentModel.DataAnnotations;

namespace CVGS.ViewModels
{
    public class CreditCardViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        [DataType(DataType.Date)]
        public DateTime Expiry { get; set; }
        public string Code { get; set; }
        [Display(Name = "Card Type")]
        public string Brand { get; set; }

        public string Address { get; set; }
    }
}
