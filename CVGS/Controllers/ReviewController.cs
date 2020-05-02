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
    public class ReviewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReviewController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Review
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
                    var review = _context.Review.Include(x => x.User).Include(x => x.Game).OrderBy(x => x.Approved).ThenBy(x => x.Game.Name).ToListAsync();
                    return View(await review);
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

        // POST: Review/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GameId,UserId,Text,Date,Recommended")] Review review)
        {
            if (ModelState.IsValid)
            {
                foreach (var r in _context.Review)
                {
                    if (r.GameId == review.GameId && r.UserId == review.UserId)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Home", new { id = review.GameId});
            }
            return RedirectToAction("index", "home");
        }

        // GET: Review/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
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
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var review = await _context.Review.Include(x => x.User).Include(x => x.Game).FirstOrDefaultAsync(x => x.Id == id);
                    if (review == null)
                    {
                        return NotFound();
                    }
                    return View(review);
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

        // POST: Review/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,GameId,UserId,Text,Date,Recommended,Approved")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (review.Approved == null)
                {
                    review.Approved = false;
                }
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Review/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                if (id == null)
                {
                    return NotFound();
                }

                var review = await _context.Review
                    .Include(x => x.User)
                    .Include(x => x.Game)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (review == null)
                {
                    return NotFound();
                }
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId == userId) || review.UserId == userId)
                {
                    return View(review);
                }
                //if regular user, redirect to home
                else
                {
                    return NotFound();
                }


            }
            //if user is not logged in, direct to 404
            else
            {
                return NotFound();
            }
            
        }

        // POST: Review/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var review = await _context.Review.FindAsync(id);
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(Guid id)
        {
            return _context.Review.Any(e => e.Id == id);
        }

        public async Task<IActionResult> Update(Guid? id)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                if (id == null)
                {
                    return NotFound();
                }

                var review = await _context.Review.Include(x => x.User).Include(x => x.Game).FirstOrDefaultAsync(x => x.Id == id);
                if (review == null)
                {
                    return NotFound();
                }
                if (review.UserId == userId)
                {
                    return View(review);
                }
                //if regular user, redirect to home
                else
                {
                    return NotFound();
                }
            }
            //if user is not logged in, direct to 404
            else
            {
                return NotFound();
            }
        }

        // POST: Review/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Guid id, [Bind("Id,GameId,UserId,Text,Date,Recommended,Approved")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }
    }
}
