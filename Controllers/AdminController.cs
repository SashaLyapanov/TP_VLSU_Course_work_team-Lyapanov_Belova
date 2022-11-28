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
    public class AdminController : Controller
    {
        private readonly TravelAgencyContext _context;

        public AdminController(TravelAgencyContext context)
        { 
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null || _context.administrators == null)
            {
                return NotFound();
            }

            var administrator = await _context.administrators
                .FirstOrDefaultAsync(m => m.Id == id);
            if (administrator == null)
            {
                return NotFound();
            }

            return View(administrator);
        }


    }
}

