using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TravelAgency_Prod.Data;
using TravelAgency_Prod.Models;

namespace TravelAgency_Prod.Controllers
{
    public class AdminCRUDTourManagersController : Controller
    {
        private readonly TravelAgencyContext _context;

        public AdminCRUDTourManagersController(TravelAgencyContext context)
        {
            _context = context;
        }





        public async Task<IActionResult> EmailValidation([Bind("Email")] TourManager tourManager)
        {
            if (_context.tourManagers.Any(x => x.Email == tourManager.Email))
            {
                return Json(false);
            }
            return Json(true);

        }





        // GET: AdminCRUDTourManagers
        public async Task<IActionResult> Index()
        {
            return View(await _context.tourManagers.ToListAsync());
        }

        // GET: AdminCRUDTourManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tourManagers == null)
            {
                return NotFound();
            }

            var tourManager = await _context.tourManagers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tourManager == null)
            {
                return NotFound();
            }

            return View(tourManager);
        }

        // GET: AdminCRUDTourManagers/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: AdminCRUDTourManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Surname,Name,Patronymic,Email,Password, RoleId")] TourManager tourManager)
        {


            _context.Add(tourManager);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            //return View(tourManager);
        }

        // GET: AdminCRUDTourManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tourManagers == null)
            {
                return NotFound();
            }

            var tourManager = await _context.tourManagers.FindAsync(id);
            if (tourManager == null)
            {
                return NotFound();
            }
            return View(tourManager);
        }

        // POST: AdminCRUDTourManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Surname,Name,Patronymic,Email,Password")] TourManager tourManager)
        {
            if (id != tourManager.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tourManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourManagerExists(tourManager.Id))
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
            return View(tourManager);
        }

        // GET: AdminCRUDTourManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tourManagers == null)
            {
                return NotFound();
            }

            var tourManager = await _context.tourManagers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tourManager == null)
            {
                return NotFound();
            }

            return View(tourManager);
        }

        // POST: AdminCRUDTourManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tourManagers == null)
            {
                return Problem("Entity set 'DBContext.tourManagers'  is null.");
            }
            var tourManager = await _context.tourManagers.FindAsync(id);
            if (tourManager != null)
            {
                _context.tourManagers.Remove(tourManager);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourManagerExists(int id)
        {
            return _context.tourManagers.Any(e => e.Id == id);
        }
    }
}

