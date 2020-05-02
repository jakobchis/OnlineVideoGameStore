using CVGS.Data;
using CVGS.Models;
using CVGS.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CVGS.ViewComponents
{
	public class GenderViewComponent : ViewComponent
	{
		private readonly ApplicationDbContext _context;

		public GenderViewComponent(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync(string id)
		{
			List<SelectListItem> items = new List<SelectListItem>();
			var genders = await _context.Gender.ToListAsync();
			foreach (Gender g in genders)
			{
				items.Add(new SelectListItem {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                    Selected = (id == g.Id.ToString() ? true : false)
                });
			}

			return View(items);
		}
	}
}
