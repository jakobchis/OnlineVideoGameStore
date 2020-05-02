using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        public ApplicationUser User { get; set; }
    }
}
