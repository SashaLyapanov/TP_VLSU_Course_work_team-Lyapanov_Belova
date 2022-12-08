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

       

        public async Task<IActionResult> Index(int id)
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



        public async Task<IActionResult> ToursListForClients()
        {
            var tours = _context.tours;
            return View(tours);
        }



        public IActionResult TourDetail(int id)
        {


            if (id == null || _context.tourManagers == null)
            {
                return NotFound();
            }

            var tour = _context.tours.FirstOrDefault(t => t.Id == id);
            if (tour == null)
            {
                return NotFound();
            }

            Response.Cookies.Append("tourId", id.ToString());
            return View(tour);
        }


        public async Task<IActionResult> SuccessfullAddInBasket()
        {
            Basket basket = new Basket();
            basket.TourId = Convert.ToInt32(Request.Cookies["tourId"]);
            basket.ClientId = Convert.ToInt32(Request.Cookies["userId"]);
            TempData["userIdToIndex"] = Convert.ToInt32(Request.Cookies["userId"]);
            //Удаление куки
            Response.Cookies.Delete("tourId");

            _context.baskets.Add(basket);
            await _context.SaveChangesAsync();

            return View();
        }

        public async Task<IActionResult> AboutUs()
        {
            return View();
        }

        public async Task<IActionResult> Contacts()
        {
            return View();
        }


        public async Task<IActionResult> Basket()
        {

            var baskets = _context.baskets;
            return View(baskets);
        }
    }
}
