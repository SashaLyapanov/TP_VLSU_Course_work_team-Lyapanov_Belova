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
    public class ClientsController : Controller
    {
        private readonly TravelAgencyContext _context;

        public ClientsController(TravelAgencyContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null || _context.clients == null)
            {
                return NotFound();
            }

            var client = await _context.clients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }
    }
}
