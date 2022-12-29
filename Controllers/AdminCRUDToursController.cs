using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
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
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.tours.ToListAsync());
        }*/

        public async Task<IActionResult> Index()
        {
            var tours = _context.tours.ToList();
            return View(tours);
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
        public async Task<IActionResult> Create([Bind("Id,Name,Country,City,PersonCount,Cost,DepartmentDate,ReturnDate,Available,Hotel,CategoryId,img,Description")] Tour tour)
        {          
            tour.Duration = Convert.ToInt32((tour.ReturnDate).DayOfYear - (tour.DepartmentDate).DayOfYear);
            _context.Add(tour);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,City,PersonCount,Cost,DepartmentDate,ReturnDate,Available,Hotel,CategoryId,img,Description")] Tour tour)
        {
            if (id != tour.Id)
            {
                return NotFound();
            }

            
                try
                {
                    tour.Duration = Convert.ToInt32((tour.ReturnDate).DayOfYear - (tour.DepartmentDate).DayOfYear);
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
