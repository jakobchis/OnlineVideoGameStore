using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    // "Library" is a nice name for "UserGame"
    public class Library
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GameDigitalId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateAcquired { get; set; }

        public ApplicationUser User { get; set; }
        public GameDigital GameDigital { get; set; }
    }
}
