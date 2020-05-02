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
	public class RateViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;

        public RateViewComponent(ApplicationDbContext context)
		{
			_context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid Id)
		{
            decimal rateTotal = _context.Review.Count(x => x.GameId == Id && x.Approved == true);
            decimal ratePositive = _context.Review.Count(x => x.GameId == Id && x.Approved == true && x.Recommended == true);
            decimal ratePercentDecimal = 0;
            int ratePercent = 0;
            if (rateTotal != 0)
            {
                ratePercentDecimal = Math.Round(ratePositive / rateTotal * 100, 0);
                ratePercent = Decimal.ToInt32(ratePercentDecimal);
                ViewData["RatePercent"] = ratePercent + "% of the " + rateTotal + " user reviews of this game are positive.";
            }
            else
            {
                ViewData["RatePercent"] = "No user reviews yet.";
            }
            return View("Default");
		}
	}
}