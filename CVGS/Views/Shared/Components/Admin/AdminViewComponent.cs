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
	public class AdminViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
            _userManager = userManager;
		}
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IViewComponentResult> InvokeAsync()
		{
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                if (_context.Admin.Any(x => x.UserId.ToString() == userId.ToString()))
                {
                    ViewBag.Admin = true;
                    return View("Default");
                }
                ViewBag.Admin = false;
                return View();
            }
            ViewBag.Admin = false;
            return View();
		}
	}
}
