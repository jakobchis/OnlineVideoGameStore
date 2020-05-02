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

namespace CVGS.Controllers
{
    public class AddressController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddressController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Address
        public async Task<IActionResult> Index()
        {
            //get user id
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;

            var addresses = await _context.Address
                                            .Include(x => x.Country)
                                            .Include(x => x.Province)
                                            .Where(x => x.UserId == userId)
                                            .ToListAsync();
            return View(addresses);
        }

        [HttpPost]
        public JsonResult GetProvinces(Guid countryId)
        {
            var Provinces = _context.Province.Where(x => x.CountryId == countryId).ToList();
            SelectList selectList = new SelectList(Provinces, "Id", "Name", 0);
            return Json(selectList);
        }

        // GET: Address/Create
        public IActionResult Create()
        {
            CreateAddressViewModel vm = new CreateAddressViewModel();
            var canada = _context.Country.FirstOrDefault(x => x.Name == "Canada");
            vm.CountryId = canada.Id.ToString();
            vm.ProvinceList = _context.Province.Where(x => x.CountryId == canada.Id).ToList();
            vm.CountryList = _context.Country.ToList();
            return View(vm);
        }

        // POST: Address/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,CompanyName,Street,Unit,City,ProvinceId,CountryId,Zip")] Address address)
        {
            address.UserId = (await GetCurrentUserAsync()).Id;

            if (ModelState.IsValid)
            {
                _context.Add(address);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(address);
        }

        // GET: Address/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            CreateAddressViewModel vm = new CreateAddressViewModel();
            vm.FirstName = address.FirstName;
            vm.LastName = address.LastName;
            vm.CompanyName = address.CompanyName;
            vm.Street = address.Street;
            vm.Unit = address.Unit;
            vm.City = address.City;
            vm.ProvinceId = address.ProvinceId;
            vm.CountryId = address.CountryId;
            vm.Zip = address.Zip;

            vm.ProvinceList = _context.Province.Where(x => x.CountryId == Guid.Parse(vm.CountryId)).ToList();
            vm.CountryList = _context.Country.ToList();

            return View(vm);
        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FirstName,LastName,CompanyName,Street,Unit,City,ProvinceId,CountryId,Zip")] Address address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
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
            return View(address);
        }

        // GET: Address/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var address = await _context.Address
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var address = await _context.Address.FindAsync(id);
            _context.Address.Remove(address);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(Guid id)
        {
            return _context.Address.Any(e => e.Id == id);
        }
    }
}
