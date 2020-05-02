using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class EventUser
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateRegistered { get; set; }

        public Event Event { get; set; }
        public ApplicationUser User { get; set; }
    }
}
