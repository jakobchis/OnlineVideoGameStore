using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class Wishlist
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public Game Game { get; set; }
        public ApplicationUser User { get; set; }
    }
}
