using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVGS.Data;
using CVGS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CVGS.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Checkout
        public async Task<IActionResult> Index()
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    var applicationDbContext = _context.Checkout.Include(c => c.Card).Include(c => c.User).Include(c => c.Game);
                    return View(await applicationDbContext.ToListAsync());
                }
                //if regular user, redirect to home
                else
                {
                    return RedirectToAction("index", "home");
                }
            }
            //if user is not logged in, direct to 404
            else
            {
                return NotFound();
            }

        }
        
        // GET: Checkout/Create
        public async Task<IActionResult> Create()
        {
            

            if (_session.GetString("cart") != null)
            {
                List<String> gameIdList = _session.GetString("cart").Split('.').ToList();
                Checkout[] checkout = new Checkout[gameIdList.Count() - 1];

                for (int i = 0; i < checkout.Count(); i++)
                {
                    if (gameIdList[i] != "")
                    {
                        checkout[i] = new Checkout();

                        checkout[i].Game = new Game();
                        checkout[i].Game = await _context.Game
                            .FirstOrDefaultAsync(m => m.Id == Guid.Parse(gameIdList[i]));

                        checkout[i].User = new ApplicationUser();
                        checkout[i].User = await GetCurrentUserAsync();

                        checkout[i].Card = new CreditCard();
                        checkout[i].Card = await _context.CreditCard
                            .FirstOrDefaultAsync(m => m.UserId == checkout[i].User.Id);

                        checkout[i].OrderDate = new DateTime();
                        checkout[i].OrderDate = DateTime.Now;
                    }
                }
                ViewData["checkout"] = checkout;
            }
            else
            {
                return RedirectToAction("Index", "home");
            }
            
            return View();
        }

        // POST: Checkout/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,CreditCardId")] Checkout checkout)
        {
            if (ModelState.IsValid && checkout.CreditCardId.ToString() != "")
            {
                String[] gamesList = _session.GetString("cart").Split(".");
                foreach (var gameId in gamesList)
                {
                    if (gameId != "")
                    {
                        checkout.Id = Guid.NewGuid();

                        checkout.GameId = Guid.Parse(gameId);
                        checkout.OrderDate = DateTime.Now;
                        checkout.Processed = false;

                        _context.Add(checkout);
                        await _context.SaveChangesAsync();
                    }
                }
                _session.SetString("cart", "");
                _session.SetString("cartList", "");
                ViewData["checkout"] = "";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["noCreditCard"] = "Add a credit card";
            }

            return RedirectToAction(nameof(Create));
        }

        private bool CheckoutExists(Guid id)
        {
            return _context.Checkout.Any(e => e.Id == id);
        }
    }
}
