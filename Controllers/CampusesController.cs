using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EduCoreSuite.Data;
using EduCoreSuite.Models;

namespace EduCoreSuite.Controllers
{
    public class CampusesController : Controller
    {
        private readonly ForgeDBContext _context;

        public CampusesController(ForgeDBContext context)
        {
            _context = context;
        }

        // GET: Campuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Campuses.ToListAsync());
        }

        // GET: Campuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campus == null)
            {
                return NotFound();
            }

            return View(campus);
        }

        // GET: Campuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Campuses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Name,County,Constituency,Town,PhysicalAddress,PhoneNumber,Email,WebsiteURL,PostalAddress,PrincipalName,TVETRegistrationNumber,KUCCPSCode,IsMainCampus,IsActive")] Campus campus)
        {
            // Check for duplicate Code
            if (await _context.Campuses.AnyAsync(c => c.Code.ToLower() == campus.Code.ToLower()))
            {
                ModelState.AddModelError("Code", "A campus with this code already exists.");
            }

            // Check for duplicate Name
            if (await _context.Campuses.AnyAsync(c => c.Name.ToLower() == campus.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "A campus with this name already exists.");
            }

            // Check for duplicate Email (if provided)
            if (!string.IsNullOrWhiteSpace(campus.Email) && 
                await _context.Campuses.AnyAsync(c => c.Email != null && c.Email.ToLower() == campus.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "A campus with this email already exists.");
            }

            // Check for duplicate Phone Number (if provided)
            if (!string.IsNullOrWhiteSpace(campus.PhoneNumber) && 
                await _context.Campuses.AnyAsync(c => c.PhoneNumber != null && c.PhoneNumber == campus.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "A campus with this phone number already exists.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(campus);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(campus);
        }

        // GET: Campuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campuses.FindAsync(id);
            if (campus == null)
            {
                return NotFound();
            }
            return View(campus);
        }

        // POST: Campuses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Name,County,Constituency,Town,PhysicalAddress,PhoneNumber,Email,WebsiteURL,PostalAddress,PrincipalName,TVETRegistrationNumber,KUCCPSCode,IsMainCampus,IsActive")] Campus campus)
        {
            if (id != campus.Id)
            {
                return NotFound();
            }

            // Check for duplicate Code (excluding current campus)
            if (await _context.Campuses.AnyAsync(c => c.Id != id && c.Code.ToLower() == campus.Code.ToLower()))
            {
                ModelState.AddModelError("Code", "A campus with this code already exists.");
            }

            // Check for duplicate Name (excluding current campus)
            if (await _context.Campuses.AnyAsync(c => c.Id != id && c.Name.ToLower() == campus.Name.ToLower()))
            {
                ModelState.AddModelError("Name", "A campus with this name already exists.");
            }

            // Check for duplicate Email (if provided, excluding current campus)
            if (!string.IsNullOrWhiteSpace(campus.Email) && 
                await _context.Campuses.AnyAsync(c => c.Id != id && c.Email != null && c.Email.ToLower() == campus.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "A campus with this email already exists.");
            }

            // Check for duplicate Phone Number (if provided, excluding current campus)
            if (!string.IsNullOrWhiteSpace(campus.PhoneNumber) && 
                await _context.Campuses.AnyAsync(c => c.Id != id && c.PhoneNumber != null && c.PhoneNumber == campus.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "A campus with this phone number already exists.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(campus);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampusExists(campus.Id))
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
            return View(campus);
        }

        // GET: Campuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campus = await _context.Campuses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (campus == null)
            {
                return NotFound();
            }

            return View(campus);
        }

        // POST: Campuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var campus = await _context.Campuses.FindAsync(id);
            if (campus != null)
            {
                _context.Campuses.Remove(campus);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CampusExists(int id)
        {
            return _context.Campuses.Any(e => e.Id == id);
        }

        // AJAX endpoints for real-time duplicate checking
        [HttpGet]
        public async Task<IActionResult> CheckDuplicateCode(string code, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(code))
                return Json(new { isValid = true });

            bool isDuplicate;
            if (excludeId.HasValue)
            {
                isDuplicate = await _context.Campuses.AnyAsync(c => c.Id != excludeId && c.Code.ToLower() == code.ToLower());
            }
            else
            {
                isDuplicate = await _context.Campuses.AnyAsync(c => c.Code.ToLower() == code.ToLower());
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "A campus with this code already exists." : "" });
        }

        [HttpGet]
        public async Task<IActionResult> CheckDuplicateName(string name, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Json(new { isValid = true });

            bool isDuplicate;
            if (excludeId.HasValue)
            {
                isDuplicate = await _context.Campuses.AnyAsync(c => c.Id != excludeId && c.Name.ToLower() == name.ToLower());
            }
            else
            {
                isDuplicate = await _context.Campuses.AnyAsync(c => c.Name.ToLower() == name.ToLower());
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "A campus with this name already exists." : "" });
        }

        [HttpGet]
        public async Task<IActionResult> CheckDuplicateEmail(string email, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Json(new { isValid = true });

            bool isDuplicate;
            if (excludeId.HasValue)
            {
                isDuplicate = await _context.Campuses.AnyAsync(c => c.Id != excludeId && c.Email != null && c.Email.ToLower() == email.ToLower());
            }
            else
            {
                isDuplicate = await _context.Campuses.AnyAsync(c => c.Email != null && c.Email.ToLower() == email.ToLower());
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "A campus with this email already exists." : "" });
        }

        [HttpGet]
        public async Task<IActionResult> CheckDuplicatePhone(string phone, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return Json(new { isValid = true });

            bool isDuplicate;
            if (excludeId.HasValue)
            {
                isDuplicate = await _context.Campuses.AnyAsync(c => c.Id != excludeId && c.PhoneNumber != null && c.PhoneNumber == phone);
            }
            else
            {
                isDuplicate = await _context.Campuses.AnyAsync(c => c.PhoneNumber != null && c.PhoneNumber == phone);
            }

            return Json(new { isValid = !isDuplicate, message = isDuplicate ? "A campus with this phone number already exists." : "" });
        }
    }
}
