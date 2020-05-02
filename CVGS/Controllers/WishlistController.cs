using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVGS.Data;
using CVGS.Models;
using Microsoft.AspNetCore.Identity;

namespace CVGS.Controllers
{
    public class WishlistController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;

		public WishlistController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
			_userManager = userManager;
		}

		private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

		// GET: Wishlist
		public async Task<IActionResult> Index()
        {
			if (User.Identity.IsAuthenticated)
			{
				//get user id
				var user = await GetCurrentUserAsync();
				var userId = user?.Id;
				var wishlist = await _context.Wishlist.ToListAsync();

				foreach (Wishlist w in wishlist)
				{
					w.Game = _context.Game.Single(g => g.Id == w.GameId);
				}

				return View(wishlist.Where(w => w.UserId == userId));
			}
			else
			{
				return NotFound();
			}
        }

		// GET: Wishlist for a friend
		public async Task<IActionResult> IndexFriend(Guid friendId)
		{
			if (User.Identity.IsAuthenticated)
			{
				var wishlist = await _context.Wishlist.ToListAsync();

				foreach (Wishlist w in wishlist)
				{
					w.Game = _context.Game.Single(g => g.Id == w.GameId);
				}

				return View(wishlist.Where(w => w.UserId == friendId));
			}
			else
			{
				return NotFound();
			}
		}

        // GET: Wishlist/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
			if (User.Identity.IsAuthenticated)
			{
				if (id == null)
				{
					return NotFound();
				}

				var wishlist = await _context.Wishlist
					.FirstOrDefaultAsync(m => m.Id == id);
				if (wishlist == null)
				{
					return NotFound();
				}

				return View(wishlist);
			}
			else
			{
				return NotFound();
			}
        }

        // GET: Wishlist/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Wishlist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameId,UserId,Date")] Wishlist wishlist)
        {
			if (User.Identity.IsAuthenticated)
			{
				if (ModelState.IsValid)
				{
					foreach (var w in _context.Wishlist)
					{
						if (w.GameId == wishlist.GameId && w.UserId == wishlist.UserId)
						{
							return RedirectToAction(nameof(Index));
						}
					}

					_context.Add(wishlist);
					await _context.SaveChangesAsync();

					return RedirectToAction(nameof(Index));
				}
				return View(wishlist);
			}
			else
			{
				return NotFound();
			}
        }

        // GET: Wishlist/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
			if (User.Identity.IsAuthenticated)
			{
				if (id == null)
				{
					return NotFound();
				}

				var wishlist = await _context.Wishlist
					.FirstOrDefaultAsync(m => m.Id == id);
				if (wishlist == null)
				{
					return NotFound();
				}

				return View(wishlist);
			}
			else
			{
				return NotFound();
			}
        }

        // POST: Wishlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
			if (User.Identity.IsAuthenticated)
			{
				//get user id
				var user = await GetCurrentUserAsync();
				var userId = user?.Id;

				var wishlist = await _context.Wishlist.FindAsync(id);

				if (wishlist.UserId == userId)
				{
					_context.Wishlist.Remove(wishlist);
					await _context.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
				else
				{
					return NotFound();
				}
			}
			else
			{
				return NotFound();
			}
        }

        private bool WishlistExists(Guid id)
        {
            return _context.Wishlist.Any(e => e.Id == id);
        }
    }
}
