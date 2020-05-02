using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class OrderGamePhysical
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid GamePhysicalId { get; set; }
        public int Price { get; set; }
        public Guid OrderStatusId { get; set; }

        public Order Order { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public GamePhysical GamePhysical { get; set; }
    }
}
