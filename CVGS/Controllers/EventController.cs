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
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EventController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Event
        public async Task<IActionResult> Index()
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId.ToString() == userId.ToString()))
                {
                    var events = await _context.Event.Include(x => x.Admin).ToListAsync();
                    foreach(var i in events)
                    {
                        i.Admin.User = await _userManager.FindByIdAsync(i.Admin.UserId.ToString());
                    }
                    return View(events);
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

        // GET: Event/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId.ToString() == userId.ToString()))
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var @event = await _context.Event
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (@event == null)
                    {
                        return NotFound();
                    }

                    return View(@event);
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

        // GET: Event/Create
        public async Task<IActionResult> Create()
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId.ToString() == userId.ToString()))
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

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,DatePosted,DateTimeStart,DateTimeEnd")] Event @event)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                var userAdmin = _context.Admin.FirstOrDefault(x => x.UserId.ToString() == userId.ToString());
                //if user id exist in admin table, proceed
                if (userAdmin != null)
                {
                    var userAdminId = userAdmin.Id;
                    @event.AdminId = userAdminId;
                    if (ModelState.IsValid)
                    {
                        _context.Add(@event);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(@event);
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

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId.ToString() == userId.ToString()))
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var @event = await _context.Event.FindAsync(id);
                    if (@event == null)
                    {
                        return NotFound();
                    }
                    return View(@event);
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

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,AdminId,Name,Description,DatePosted,DateTimeStart,DateTimeEnd")] Event @event)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId.ToString() == userId.ToString()))
                {
                    if (id != @event.Id)
                    {
                        return NotFound();
                    }

                    var _event = await _context.Event.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            @event.AdminId = _event.AdminId;
                            _context.Update(@event);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!EventExists(@event.Id))
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
                    return View(@event);
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

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId.ToString() == userId.ToString()))
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var @event = await _context.Event
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (@event == null)
                    {
                        return NotFound();
                    }

                    return View(@event);
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

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId.ToString() == userId.ToString()))
                {
                    var @event = await _context.Event.FindAsync(id);
                    _context.Event.Remove(@event);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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

        private bool EventExists(Guid id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
