using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

namespace CVGS.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        //inherited:
        //public Guid Id { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        [PersonalData]
        public string Phone { get; set; }
        [DataType(DataType.Date), PersonalData]
        public DateTime DateOfBirth { get; set; }
        [PersonalData]
        public Guid? AddressId { get; set; }
        public bool PC { get; set; }
        public bool PS4 { get; set; }
        public bool XboxOne { get; set; }
        public bool Switch { get; set; }
        public bool Promotional { get; set; }
        [PersonalData]
        public Guid? GenderId { get; set; }
        [PersonalData]
        public Address Address { get; set; }
        [PersonalData]
        public Gender Gender { get; set; }
    }
}
