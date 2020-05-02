using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Singleplayer { get; set; }
        public bool Multiplayer { get; set; }
        public bool VR { get; set; }
        public bool Chalices { get; set; }
        public decimal Price { get; set; }

        public ICollection<GameGenre> GameGenres { get; set; }
        public ICollection<GameImage> GameImages { get; set; }
    }
}
