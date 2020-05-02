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
using Microsoft.AspNetCore.Http;

namespace CVGS.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public CartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;

            var game = await _context.Game.ToListAsync();

            if (_session.GetString("cart") != null)
            {
                List<String> cartList = _session.GetString("cart").Split('.').ToList();
                ViewData["cartList"] = cartList;
            }
            else
            {
                ViewData["cartList"] = null;
            }

            return View(game);
        }

        // POST: Cart/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameId,UserId")] Cart cart)
        {
            if (ModelState.IsValid)
            {
                if (_session.GetString("cart") != null)
                {
                    List<String> cartList = _session.GetString("cart").Split('.').ToList();
                    foreach (var cartItem in cartList)
                    {
                        if (cart.GameId.ToString() == cartItem)
                        {
                            ViewData["cartList"] = cartList;
                            return RedirectToAction(nameof(Index));
                        }
                    }
                    _session.SetString("cart", _session.GetString("cart") +  cart.GameId + ".");
                    ViewData["cartList"] = cartList;
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _session.SetString("cart", cart.GameId + ".");
                    List<String> cartList = new List<string>();
                    cartList.Add(cart.GameId.ToString());
                    ViewData["cartList"] = cartList;
                    return RedirectToAction(nameof(Index));
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Cart/Delete/5
        public async Task<IActionResult> Delete(String id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TempData["CartDeleteId"] = id;
            var game = await _context.Game
                .FirstOrDefaultAsync(m => m.Id == Guid.Parse(id));

            return View(game);
        }

        // POST: Cart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            List<String> cartList = _session.GetString("cart").Split('.').ToList();
            cartList.Remove(id);
            _session.SetString("cart", "");
            foreach (var cartItem in cartList)
            {
                _session.SetString("cart", _session.GetString("cart") + cartItem + ".");
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CartExists(Guid id)
        {
            return _context.Cart.Any(e => e.Id == id);
        }
    }
}
