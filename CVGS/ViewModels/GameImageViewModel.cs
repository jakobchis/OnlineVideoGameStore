using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Models;
using System.ComponentModel.DataAnnotations;

namespace CVGS.ViewModels
{
    public class GameImageViewModel
    {
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public string Name { get; set; }
        [Required(ErrorMessage = "Image is required.")]
        public IFormFile File { get; set; }
        public string Type { get; set; }

        public Game Game { get; set; }
    }
}
