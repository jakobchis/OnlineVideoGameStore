using CVGS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.ViewModels
{
    public class GameStore
    {
        [Key]
        public Guid GameId { get; set; }
        public string GameName { get; set; }
        public string Description { get; set; }
        public bool Singleplayer { get; set; }
        public bool Multiplayer { get; set; }
        public bool VR { get; set; }
        public bool Chalices { get; set; }
        public string Genre { get; set; }
        public byte[] GameImage { get; set; }
        public decimal Price { get; set; }
        [NotMapped]
        public List<string> GameGenreList { get; set; }
    }
}
