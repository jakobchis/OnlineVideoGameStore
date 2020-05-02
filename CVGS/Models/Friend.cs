using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class Friend
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid OtherUserId { get; set; }
        public bool Accepted { get; set; }
        public bool Family { get; set; }

        public ApplicationUser User { get; set; }
        public ApplicationUser OtherUser { get; set; }
    }
}
