using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CVGS.Data;
using CVGS.Models;

namespace CVGS.Controllers
{
    public class FriendController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FriendController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Friend
        public async Task<IActionResult> Index()
        {
            //Without this loop, not all usernames show up for whatever reason
            foreach (var item in _context.Users)
            {

            }
            GetActiveUserId();
            return View(await _context.Friend.ToListAsync());
        }

        // POST: Friend/Index
        [HttpPost]
        public async Task<IActionResult> Index(string username)
        {
            Friend friend = new Friend();
            ApplicationUser user = new ApplicationUser();
            ApplicationUser otherUser = new ApplicationUser();

            //Checks if search sent was blank
            if (username != null)
            {
                foreach (var item in _context.Users)
                {
                    //Gather Friend information
                    if (item.UserName.ToUpper() == username.ToUpper())
                    {
                        friend.OtherUserId = item.Id;
                        otherUser = _context.Users.Find(item.Id);
                        friend.OtherUser = otherUser;
                    }
                    //Gather active user information
                    if (item.UserName.ToUpper() == User.Identity.Name.ToUpper())
                    {
                        friend.UserId = item.Id;
                        user = _context.Users.Find(item.Id);
                        friend.User = user;
                    }
                }
            }
            else
            {
                //Without this loop, not all usernames show up for whatever reason
                foreach (var item in _context.Users)
                {

                }
                //No name was entered
                TempData["friendError"] = "Please enter a username";

                GetActiveUserId();
                return View(await _context.Friend.ToListAsync());
            }

            if (friend.UserId != null && friend.OtherUserId != null)
            {
                foreach (var friend1 in _context.Friend)
                {
                    //Checks if friend has already been added to list
                    if (friend.UserId == friend1.UserId && friend.OtherUserId == friend1.OtherUserId)
                    {
                        //Error message
                        TempData["friendError"] = "" + username + " has already been added to your friends list";

                        GetActiveUserId();
                        return View(await _context.Friend.ToListAsync());
                    }
                }


                GetActiveUserId();
                _context.Add(friend);
                await _context.SaveChangesAsync();
                return View(await _context.Friend.ToListAsync());
            }

            //If friend not found return to index with error
            TempData["friendError"] = "No user with the username of " + username + " was found";

            GetActiveUserId();
            return View(await _context.Friend.ToListAsync());
        }

        // GET: Friend/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var friend = await _context.Friend
                .FirstOrDefaultAsync(m => m.Id == id);
            if (friend == null)
            {
                return NotFound();
            }

            foreach (var item in _context.Users)
            {
                if (item.Id == friend.OtherUserId)
                {
                    ViewData["friendToDeleteName"] = item.UserName;
                    break;
                }
            }

            return View(friend);
        }

        // POST: Friend/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var friend = await _context.Friend.FindAsync(id);
            _context.Friend.Remove(friend);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void GetActiveUserId()
        {
            //Gets active users' Id for view
            foreach (var item in _context.Users)
            {
                if (item.UserName == User.Identity.Name)
                {
                    TempData["activeUserId"] = item.Id;
                    break;
                }
            }
        }
    }
}