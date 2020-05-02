using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CVGS.Models;
using CVGS.Data;
using CVGS.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CVGS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameStore
        public async Task<IActionResult> Index()
        {
            var gameStoreQuery = from ga in _context.Game
                                 join gg in _context.GameGenre on ga.Id equals gg.GameId into sub1
                                 from gg in sub1.DefaultIfEmpty()
                                 join ge in _context.Genre on gg.GenreId equals ge.Id into sub2
                                 from ge in sub2.DefaultIfEmpty()
                                 select new GameStore
                                 {
                                     GameId = ga.Id,
                                     GameName = ga.Name,
                                     Description = ga.Description,
                                     Singleplayer = ga.Singleplayer,
                                     Multiplayer = ga.Multiplayer,
                                     VR = ga.VR,
                                     Chalices = ga.Chalices,
                                     Genre = ge.Name,
                                     Price = ga.Price
                                 };

            var gameStore = gameStoreQuery.GroupBy(
                g => new {
                    g.GameId,
                    g.GameName,
                    g.Description,
                    g.Singleplayer,
                    g.Multiplayer,
                    g.VR,
                    g.Chalices,
                    g.Price
                },
                g => g.Genre,
                (key, group) => new GameStore
                {
                    GameId = key.GameId,
                    GameName = key.GameName,
                    Description = key.Description,
                    Singleplayer = key.Singleplayer,
                    Multiplayer = key.Multiplayer,
                    VR = key.VR,
                    Chalices = key.Chalices,
                    Price = key.Price,
                    GameGenreList = group.ToList()
                });
            return View(await gameStore.ToListAsync());
        }

        // GET: Game/Details/5
        public async Task<IActionResult> Details(Guid? id)
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
            ViewData["GameName"] = game.Name;
            var gameStoreQuery = from ga in _context.Game
                                 join gg in _context.GameGenre on ga.Id equals gg.GameId into sub1
                                 from gg in sub1.DefaultIfEmpty()
                                 join ge in _context.Genre on gg.GenreId equals ge.Id into sub2
                                 from ge in sub2.DefaultIfEmpty()
                                 where ga.Id == id
                                 select new GameStore
                                 {
                                     GameId = ga.Id,
                                     GameName = ga.Name,
                                     Description = ga.Description,
                                     Singleplayer = ga.Singleplayer,
                                     Multiplayer = ga.Multiplayer,
                                     VR = ga.VR,
                                     Chalices = ga.Chalices,
                                     Genre = ge.Name,
                                     Price = ga.Price
                                 };

            var gameStore = gameStoreQuery.GroupBy(
                g => new {
                    g.GameId,
                    g.GameName,
                    g.Description,
                    g.Singleplayer,
                    g.Multiplayer,
                    g.VR,
                    g.Chalices,
                    g.Price
                },
                g => g.Genre,
                (key, group) => new GameStore
                {
                    GameId = key.GameId,
                    GameName = key.GameName,
                    Description = key.Description,
                    Singleplayer = key.Singleplayer,
                    Multiplayer = key.Multiplayer,
                    VR = key.VR,
                    Chalices = key.Chalices,
                    Price = key.Price,
                    GameGenreList = group.ToList()
                });

            return View(await gameStore.ToListAsync());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
