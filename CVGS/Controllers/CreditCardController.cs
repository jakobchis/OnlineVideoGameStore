using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVGS.Data;
using CVGS.Models;
using CVGS.ViewModels;
using Microsoft.AspNetCore.Identity;
using CreditCardValidator;

namespace CVGS.Controllers
{
    public class CreditCardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreditCardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: CreditCard
        public async Task<IActionResult> Index()
        {
            //get user id
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;

            var _creditCards = await _context.CreditCard
                                            .Where(x => x.UserId == userId)
                                            .Include(x => x.Address)
                                            .ToListAsync();

            var creditCards = new List<CreditCardViewModel>();

            foreach (var card in _creditCards)
            {
                CreditCardDetector detector = new CreditCardDetector(card.Number.ToString());
                CreditCardViewModel creditCard = new CreditCardViewModel();
                creditCard.Id = card.Id;
                creditCard.Name = card.Name;
                creditCard.Number = "**** **** **** " + card.Number.ToString().Substring(12);
                creditCard.Expiry = card.Expiry;
                creditCard.Code = card.Code;
                creditCard.Brand = detector.BrandName;
                creditCard.Address = card.Address.Street;
                creditCards.Add(creditCard);
            }

            return View(creditCards);
        }

        // GET: CreditCard/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CreditCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Number,Expiry,Code,AddressId")] CreditCard creditCard)
        {
            creditCard.UserId = (await GetCurrentUserAsync()).Id;

            if (ModelState.IsValid)
            {
                _context.Add(creditCard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(creditCard);
        }

        // GET: CreditCard/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var creditCard = await _context.CreditCard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (creditCard == null)
            {
                return NotFound();
            }

            return View(creditCard);
        }

        // POST: CreditCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var creditCard = await _context.CreditCard.FindAsync(id);
            _context.CreditCard.Remove(creditCard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CreditCardExists(Guid id)
        {
            return _context.CreditCard.Any(e => e.Id == id);
        }
    }
}
