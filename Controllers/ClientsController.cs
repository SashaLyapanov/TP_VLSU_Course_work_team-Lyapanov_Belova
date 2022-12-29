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


        public async Task<IActionResult> ToursListWithSearch(string Country, DateTime DepartureDate, DateTime ReturnDate, string PersonCount)
        {

            var tours = from m in _context.tours select m;

            if (!String.IsNullOrEmpty(Country) && !String.IsNullOrEmpty(DepartureDate.ToString()) && !String.IsNullOrEmpty(ReturnDate.ToString()) && !String.IsNullOrEmpty(PersonCount))
            {
                tours = tours.Where(a => a.Country == Country && a.DepartmentDate == DepartureDate && a.ReturnDate == ReturnDate && a.PersonCount == Int32.Parse(PersonCount));
            }


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


        public IActionResult TourDetail1(int id)
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


        public IActionResult TourDetail2(int id)
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


        public async Task<IActionResult> SuccessfullAddInFavourite()
        {
            Favourite favourite = new Favourite();
            favourite.TourId = Convert.ToInt32(Request.Cookies["tourId"]);
            favourite.ClientId = Convert.ToInt32(Request.Cookies["userId"]);
            TempData["userIdToIndex"] = Convert.ToInt32(Request.Cookies["userId"]);
            //Удаление куки
            Response.Cookies.Delete("tourId");

            _context.favourites.Add(favourite);
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
            var BasketForClient = _context.baskets.Where(a => a.ClientId == Convert.ToInt32(Request.Cookies["userId"]));
            List<int> toursId= new List<int>();
            foreach (Basket basket in BasketForClient)
            {
                toursId.Add(basket.TourId);
            }

            var toursInBasket = _context.tours.Where(a => toursId.Contains(a.Id));
            return View(toursInBasket);
        }


        public async Task<IActionResult> Favourite()
        {
            var FavouriteForClient = _context.favourites.Where(a => a.ClientId == Convert.ToInt32(Request.Cookies["userId"]));
            List<int> toursId = new List<int>();
            foreach (Favourite favourite in FavouriteForClient)
            {
                toursId.Add(favourite.TourId);
            }

            var toursInFavourite = _context.tours.Where(a => toursId.Contains(a.Id));
            return View(toursInFavourite);
        }
    }
}
