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
using System.IO;

namespace CVGS.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GameController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Game
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
                    return View(await _context.Game.ToListAsync());
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

        // GET: Game/Details/5
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

                    var game = await _context.Game
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (game == null)
                    {
                        return NotFound();
                    }

                    return View(game);
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

        // GET: Game/Create
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

        // POST: Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Price,Singleplayer,Multiplayer,VR,Chalices")] Game game)
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
                    if (ModelState.IsValid)
                    {
                        _context.Add(game);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    return View(game);
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

        // GET: Game/Edit/5
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

                    var game = await _context.Game.FindAsync(id);
                    if (game == null)
                    {
                        return NotFound();
                    }
                    return View(game);
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

        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Price,Singleplayer,Multiplayer,VR,Chalices")] Game game)
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
                    if (id != game.Id)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(game);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!GameExists(game.Id))
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
                    return View(game);
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

        // GET: Game/Delete/5
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

                    var game = await _context.Game
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (game == null)
                    {
                        return NotFound();
                    }

                    return View(game);
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

        // POST: Game/Delete/5
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
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    var game = await _context.Game.FindAsync(id);
                    _context.Game.Remove(game);
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

        private bool GameExists(Guid id)
        {
            return _context.Game.Any(e => e.Id == id);
        }

        // GET: Game/AddGenre/{guid}
        public async Task<IActionResult> AddGenre(Guid? id)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || id == Guid.Empty)
                {
                    return NotFound();
                }
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    var genre = (from g in _context.Genre
                                 where !_context.GameGenre.Any(ga => ga.GenreId == g.Id && ga.GameId == id)
                                 select g).ToList();
                    ViewData["GenreId"] = new SelectList(genre, "Id", "Name");
                    return View();
                }
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

        // POST: Game/AddGenre/{guid}
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGenre(Guid id, [Bind("Id,GameId,GenreId")] GameGenre gameGenre)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    gameGenre.GameId = id;
                    gameGenre.Id = Guid.NewGuid();
                    _context.Add(gameGenre);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", gameGenre.GenreId);
                return View(gameGenre);
            }
            //if user is not logged in, direct to 404
            else
            {
                return NotFound();
            }

        }

        // GET: Game/RemoveGameGenre/{guid}
        public async Task<IActionResult> RemoveGameGenre(Guid? id)
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
                    if (id == null || id == Guid.Empty)
                    {
                        return NotFound();
                    }

                    var gameGenre = await _context.GameGenre.Include(x => x.Game).Include(x => x.Genre)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (gameGenre == null)
                    {
                        return NotFound();
                    }

                    return View(gameGenre);
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

        // POST: Game/RemoveGameGenre/{guid}
        [HttpPost, ActionName("RemoveGameGenre")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveGameGenreConfirmed(Guid id)
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
                    var gameGenre = await _context.GameGenre.FindAsync(id);
                    _context.GameGenre.Remove(gameGenre);
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

        // GET: Game/AddImage/{guid]
        public async Task<IActionResult> AddImage(Guid? id)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || id == Guid.Empty)
                {
                    return NotFound();
                }
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    return View(new ViewModels.GameImageViewModel());
                }
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

        // POST: Game/AddImage/{guid]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddImage(Guid id, [Bind("Id,GameId,Name,File,Type")] GameImage gameImage, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    if (file.Length > 0)
                    {
                        byte[] p1 = null;
                        using (var fs1 = file.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        gameImage.File = p1;
                        gameImage.GameId = id;
                        gameImage.Id = Guid.NewGuid();
                        _context.Add(gameImage);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    return View(new ViewModels.GameImageViewModel());
                }
            }

            return RedirectToAction("index", "home");
        }

        // GET: Game/ViewImage/{guid}
        public async Task<IActionResult> ViewImage(Guid? id)
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                if (id == null || id == Guid.Empty)
                {
                    return NotFound();
                }
                //get user id
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                //if user id exist in admin table, proceed
                if (_context.Admin.Any(x => x.UserId == userId))
                {
                    var gameImage = _context.GameImage.Include(x => x.Game).Where(x => x.GameId == id);
                    return View(await gameImage.ToListAsync());
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

        // GET: Game/RemoveGameImage/{guid}
        public async Task<IActionResult> RemoveGameImage(Guid? id)
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
                    if (id == null || id == Guid.Empty)
                    {
                        return NotFound();
                    }

                    var gameImage = await _context.GameImage
                        .Include(x => x.Game)
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (gameImage == null)
                    {
                        return NotFound();
                    }

                    return View(gameImage);
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

        // POST: Game/RemoveGameImage/{guid}
        [HttpPost, ActionName("RemoveGameImage")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveGameImageConfirmed(Guid id)
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
                    var gameImage = await _context.GameImage.FindAsync(id);
                    _context.GameImage.Remove(gameImage);
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
    }
}
