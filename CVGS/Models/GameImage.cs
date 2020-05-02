using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class GameImage
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public string Name { get; set; }
        public byte[] File { get; set; }
        public string Type { get; set; }

        public Game Game { get; set; }
    }
}
