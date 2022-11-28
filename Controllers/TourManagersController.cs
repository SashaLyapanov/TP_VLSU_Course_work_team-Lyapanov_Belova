using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TravelAgency_Prod.Data;
using TravelAgency_Prod.Models;

namespace TravelAgency.Controllers
{
    public class TourManagersController : Controller
    {
        private readonly TravelAgencyContext _context;

        public TourManagersController(TravelAgencyContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int? id)
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


    }
}
