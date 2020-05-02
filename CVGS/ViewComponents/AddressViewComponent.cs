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
using Microsoft.AspNetCore.Identity;

namespace CVGS.ViewComponents
{
    public class AddressViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddressViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            List<SelectListItem> items = new List<SelectListItem>();

            var user = await GetCurrentUserAsync();
            var userId = user?.Id;

            var addresses = await _context.Address
                                          .Where(x => x.UserId == userId)
                                          .ToListAsync();
            foreach (Address a in addresses)
            {
                items.Add(new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Street,
                    Selected = (id == a.Id.ToString() ? true : false)
                });
            }

            return View(items);
        }
    }
}
