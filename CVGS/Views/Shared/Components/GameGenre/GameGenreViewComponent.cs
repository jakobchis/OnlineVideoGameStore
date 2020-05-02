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
	public class GameGenreViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GameGenreViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
            _userManager = userManager;
		}
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
		{
            var gameGenre = (from g in _context.Genre
                                join gg in _context.GameGenre on g.Id equals gg.GenreId
                                where gg.GameId == Id
                                select new GameGenreComponentViewModel { GameGenreId = gg.Id, GenreName = g.Name }).ToListAsync();
            return View(await gameGenre);
		}
	}
}
