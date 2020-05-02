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
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Admin
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
                    var admins = await _context.Admin.Include(x => x.User).ToListAsync();
                    foreach (var i in admins)
                    {
                        i.User = await _userManager.FindByIdAsync(i.UserId.ToString());
                    }
                    return View(admins);
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

        // GET: Admin/Details/5
        public async Task<IActionResult> Details(Guid? id)
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

                    var admin = await _context.Admin
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (admin == null)
                    {
                        return NotFound();
                    }

                    return View(admin);
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

        // GET: Admin/Create
        public async Task<IActionResult> Create()
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
                    return View();
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

        // POST: Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(admin);
        }

        // GET: Admin/Edit/5
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

                    var admin = await _context.Admin.FindAsync(id);
                    if (admin == null)
                    {
                        return NotFound();
                    }
                    return View(admin);
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

        // POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserId")] Admin admin)
        {
            if (id != admin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.Id))
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
            return View(admin);
        }

        // GET: Admin/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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

                    var admin = await _context.Admin
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (admin == null)
                    {
                        return NotFound();
                    }

                    return View(admin);
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

        // POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var admin = await _context.Admin.FindAsync(id);
            _context.Admin.Remove(admin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(Guid id)
        {
            return _context.Admin.Any(e => e.Id == id);
        }
    }
}
