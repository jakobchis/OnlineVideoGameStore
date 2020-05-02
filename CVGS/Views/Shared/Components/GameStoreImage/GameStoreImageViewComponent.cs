using CVGS.Data;
using CVGS.Models;
using CVGS.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.ViewComponents
{
	public class GameStoreImageViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;

        public GameStoreImageViewComponent(ApplicationDbContext context)
		{
			_context = context;
		}

        public async Task<IViewComponentResult> InvokeAsync(Guid Id, bool isCarousel)
		{
            var gameImage = (from g in _context.Game
                             join gi in _context.GameImage on g.Id equals gi.GameId
                             where gi.GameId == Id
                             select new GameImage { File = gi.File }).ToListAsync();
            if (!isCarousel)
            {
                return View("Default", await gameImage);
            }
            else
            {
                return View("Carousel", await gameImage);
            }
            
		}
	}
}
