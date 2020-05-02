using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class OrderGameDigital
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid GameDigitalId { get; set; }
        public int Price { get; set; }

        public Order Order { get; set; }
        public GameDigital GameDigital { get; set; }
    }
}
