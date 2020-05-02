using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Models;

namespace CVGS.ViewModels
{
    public class CreateAddressViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Street { get; set; }
        public string Unit { get; set; }
        public string City { get; set; }
        public string ProvinceId { get; set; }
        public string CountryId { get; set; }
        public string Zip { get; set; }

        public Province Province { get; set; }
        public Country Country { get; set; }
        // public Province SelectedProvince { get; set; }
        // public Country SelectedCountry { get; set; }
        public IEnumerable<Province> ProvinceList { get; set; }
        public ICollection<Country> CountryList { get; set; }
    }
}
