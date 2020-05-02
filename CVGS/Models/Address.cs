using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Company Name (Optional)")]
        public string CompanyName { get; set; }
        [Display(Name = "Street Address")]
        public string Street { get; set; }
        [Display(Name = "Unit (Optional)")]
        public string Unit { get; set; }
        public string City { get; set; }
        [Display(Name = "Province")]
        public string ProvinceId { get; set; }
        [Display(Name = "Country")]
        public string CountryId { get; set; }
        public string Zip { get; set; }

        public Province Province { get; set; }
        public Country Country { get; set; }
    }
}
