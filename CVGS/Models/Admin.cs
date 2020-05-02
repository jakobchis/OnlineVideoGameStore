using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class Admin
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
