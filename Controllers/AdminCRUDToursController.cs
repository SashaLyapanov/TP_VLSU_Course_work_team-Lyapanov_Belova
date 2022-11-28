using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency_Prod.Data;
using TravelAgency_Prod.Models;

namespace TravelAgency_Prod.Controllers
{
    public class AdminCRUDToursController : Controller
    {
        private readonly TravelAgencyContext _context;

        public AdminCRUDToursController(TravelAgencyContext context)
        {
            _context = context;
        }

        // GET: AdminCRUDTours
        public async Task<IActionResult> Index()
        {
            return View(await _context.tours.ToListAsync());
        }

        // GET: AdminCRUDTours/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.tours == null)
            {
                return NotFound();
            }

            var tour = await _context.tours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // GET: AdminCRUDTours/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminCRUDTours/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,PersonCount,Cost,DepartmentDate,ReturnDate,Duration,Hotel,TourType")] Tour tour)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tour);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tour);
        }

        // GET: AdminCRUDTours/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.tours == null)
            {
                return NotFound();
            }

            var tour = await _context.tours.FindAsync(id);
            if (tour == null)
            {
                return NotFound();
            }
            return View(tour);
        }

        // POST: AdminCRUDTours/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,PersonCount,Cost,DepartmentDate,ReturnDate,Duration,Hotel,TourType")] Tour tour)
        {
            if (id != tour.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tour);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TourExists(tour.Id))
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
            return View(tour);
        }

        // GET: AdminCRUDTours/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.tours == null)
            {
                return NotFound();
            }

            var tour = await _context.tours
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }

        // POST: AdminCRUDTours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.tours == null)
            {
                return Problem("Entity set 'DBContext.tours'  is null.");
            }
            var tour = await _context.tours.FindAsync(id);
            if (tour != null)
            {
                _context.tours.Remove(tour);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TourExists(int id)
        {
            return _context.tours.Any(e => e.Id == id);
        }
    }
}
