﻿using System;
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
    public class GenreUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GenreUserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: GenreUser
        public async Task<IActionResult> Index()
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                var applicationDbContext = _context.GenreUser.Include(g => g.Genre).Where(g => g.User == user);
                return View(await applicationDbContext.ToListAsync());
            }
            //if user is not logged in, direct to 404
            else
            {
                return NotFound();
            }
            
        }

        // GET: GenreUser/Create
        public async Task<IActionResult> Create()
        {
            //if user is logged in, proceed
            if (User.Identity.IsAuthenticated)
            {
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                var genre = (from g in _context.Genre
                             where !_context.GenreUser.Any(gu => gu.GenreId == g.Id && gu.UserId == userId)
                             select g).ToList();
                ViewData["GenreId"] = new SelectList(genre, "Id", "Name");
                return View();
            }
            //if user is not logged in, direct to 404
            else
            {
                return NotFound();
            }
        }

        // POST: GenreUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GenreId,UserId")] GenreUser genreUser)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var user = await GetCurrentUserAsync();
                    var userId = user?.Id;
                    genreUser.UserId = (Guid)userId;
                    genreUser.Id = Guid.NewGuid();
                    _context.Add(genreUser);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Name", genreUser.GenreId);
                return View(genreUser);
            }
            //if user is not logged in, direct to 404
            else
            {
                return NotFound();
            }
            
        }

        // GET: GenreUser/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (id == null)
                {
                    return NotFound();
                }
                var user = await GetCurrentUserAsync();
                var userId = user?.Id;
                var genreUser = await _context.GenreUser
                    .Include(g => g.Genre)
                    .Include(g => g.User)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (genreUser == null || genreUser.UserId != userId)
                {
                    return NotFound();
                }

                return View(genreUser);
            }
            //if user is not logged in, direct to 404
            else
            {
                return NotFound();
            }
        }

        // POST: GenreUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var genreUser = await _context.GenreUser.FindAsync(id);
            _context.GenreUser.Remove(genreUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}