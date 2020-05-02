using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class Checkout
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid UserId { get; set; }
        public Guid CreditCardId { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
        public Boolean Processed { get; set; }
        
        public Game Game { get; set; }
        public ApplicationUser User { get; set; }
        public CreditCard Card { get; set; }
    }
}
