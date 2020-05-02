using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.CustomValidation;

namespace CVGS.Models
{
    public class Event
    {
        public Guid Id { get; set; }
        public Guid AdminId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [EventDateTodayValidation(ErrorMessage = "Date must be today or after.")]
        [DataType(DataType.Date)]
        public DateTime DatePosted { get; set; }
        [EventDateTodayValidation(ErrorMessage = "Date must be today or after.")]
        [EventDateCompareValidation("DatePosted")]
        [DataType(DataType.Date)]
        [Display(Name = "Event's Start Date")]
        public DateTime DateTimeStart { get; set; }
        [EventDateTodayValidation(ErrorMessage = "Date must be today or after.")]
        [EventDateCompareValidation("DateTimeStart")]
        [DataType(DataType.Date)]
        [Display(Name = "Event's End Date")]
        public DateTime DateTimeEnd { get; set; }

        public Admin Admin { get; set; }
        public ICollection<EventImage> EventImages { get; set; }
        public ICollection<EventUser> EventUsers { get; set; }
    }
}
