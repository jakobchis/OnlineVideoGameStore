using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVGS.Data;
using CVGS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CVGS.Controllers
{
    public class GenreController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GenreController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Genre
        //[Authorize]
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
                    return View(await _context.Genre.ToListAsync());
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

        // GET: Genre/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var genre = await _context.Genre
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (genre == null)
                    {
                        return NotFound();
                    }

                    return View(genre);
                }
                else
                {
                    return RedirectToAction("index", "home");
                }

            }
            else
            {
                return NotFound();
            }
            
        }

        // GET: Genre/Create
        public async Task<IActionResult> Create()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    return View();
                }
                else
                {
                    return RedirectToAction("index", "home");
                }

            }
            else
            {
                return NotFound();
            }
        }

        // POST: Genre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Genre genre)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(genre);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(genre);
                }
                else
                {
                    return RedirectToAction("index", "home");
                }

            }
            else
            {
                return NotFound();
            }
        }

        // GET: Genre/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var genre = await _context.Genre.FindAsync(id);
                    if (genre == null)
                    {
                        return NotFound();
                    }
                    return View(genre);
                }
                else
                {
                    return RedirectToAction("index", "home");
                }


            }
            else
            {
                return NotFound();
            }
        }

        // POST: Genre/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name")] Genre genre)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    if (id != genre.Id)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(genre);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!GenreExists(genre.Id))
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
                    return View(genre);
                }
                else
                {
                    return RedirectToAction("index", "home");
                }


            }
            else
            {
                return NotFound();
            }
        }

        // GET: Genre/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    //If user is admin, proceed
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var genre = await _context.Genre
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (genre == null)
                    {
                        return NotFound();
                    }

                    return View(genre);
                }
                else
                {
                    return RedirectToAction("index", "home");
                }


            }
            else
            {
                return NotFound();
            }
        }

        // POST: Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    //If user is admin, proceed
                    var genre = await _context.Genre.FindAsync(id);
                    _context.Genre.Remove(genre);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction("index", "home");
                }


            }
            else
            {
                return NotFound();
            }
        }

        private bool GenreExists(Guid id)
        {
            return _context.Genre.Any(e => e.Id == id);
        }
    }
}
