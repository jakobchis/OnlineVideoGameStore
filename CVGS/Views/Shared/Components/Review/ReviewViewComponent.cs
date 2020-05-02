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
	public class ReviewViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
		{
			_context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
		{
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                var userId = user.Id;
                var review = _context.Review.Where(x => x.UserId == userId && x.GameId == Id);
                var allReviews = (from g in _context.Game
                                  join r in _context.Review on g.Id equals r.GameId
                                  join u in _context.Users on r.UserId equals u.Id
                                  where r.GameId == Id && r.UserId != userId && r.Approved == true
                                  select new Review { Text = r.Text, Recommended = r.Recommended, Date = r.Date, User = r.User }).ToList();
                if (!review.Any())
                {
                    ViewData["ReviewGameId"] = Id;
                    return View("Default", allReviews);
                }
                else
                {
                    ViewData["ReviewId"] = review.FirstOrDefault().Id;
                    ViewData["ReviewUsername"] = user.UserName;
                    ViewData["ReviewText"] = review.FirstOrDefault().Text;
                    ViewData["ReviewDate"] = review.FirstOrDefault().Date.ToString("dd MMM, yyyy");
                    ViewData["ReviewRecommended"] = review.FirstOrDefault().Recommended;
                    return View("Exist", allReviews);
                }
            }
            else
            {
                var allReviews = (from g in _context.Game
                                  join r in _context.Review on g.Id equals r.GameId
                                  join u in _context.Users on r.UserId equals u.Id
                                  where r.GameId == Id && r.Approved == true
                                  select new Review { Text = r.Text, Recommended = r.Recommended, Date = r.Date, User = r.User }).ToList();
                return View("Default", allReviews);
            }
		}
	}
}