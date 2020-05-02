using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid CreditCardId { get; set; }
        public int PaymentAmount { get; set; }

        public Order Order { get; set; }
        public CreditCard CreditCard { get; set; }
    }
}
