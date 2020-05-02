using CVGS.Data;
using CVGS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.ViewComponents
{
	public class TopMenuButtonsViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public TopMenuButtonsViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			bool isAuthenticated;
			isAuthenticated = User.Identity.IsAuthenticated;

			return View(isAuthenticated);
		}
	}
}
