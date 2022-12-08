using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TravelAgency_Prod.Data;
using TravelAgency_Prod.Models;

namespace TravelAgency.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly TravelAgencyContext _context;

        public HomeController(ILogger<HomeController> logger, TravelAgencyContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }




        [HttpGet]
        public async Task<IActionResult> Index1(int? id)
        {
            if (id == null || _context.tourManagers == null)
            {
                return NotFound();
            }

            var client = await _context.tourManagers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }





        [HttpGet]
        public async Task<IActionResult> ToursList()
        {
            var tours = _context.tours;
            return View(tours);
        }







        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> AboutUs()
        {
            return View();
        }
    }
}