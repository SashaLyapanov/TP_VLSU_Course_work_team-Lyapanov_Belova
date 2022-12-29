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

        public async Task<IActionResult> ToursListForManager()
        {
            var tours = _context.tours.ToList();
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



        public IActionResult AddTourInClientsBasket()
        {            
            //.Where(a => a.Id == Convert.ToInt32(Request.Cookies["tourId"]));
            int id = _context.clients.Where(a => a.Id == Convert.ToInt32(Request.Cookies["clientId"])).Select(a => a.Id).FirstOrDefault();
            //_context.appointments.Where(a => a.Id == appointment.Id).Select(a => a.PatientId).FirstOrDefault();
            Client client= _context.clients.FirstOrDefault(t => t.Id == id);
            return View(client);
        }


        public async Task<IActionResult> SuccessfullAddTourInClientsBasket()
        {
            Basket tour = new Basket();
            tour.TourId = Convert.ToInt32(Request.Cookies["tourId"]);
            tour.ClientId = Convert.ToInt32(Request.Cookies["clientId"]);
            _context.baskets.Add(tour);
            
            await _context.SaveChangesAsync();


            return View();
        }



        public async Task<IActionResult> ClientsListForManager()
        {
            var clients = _context.clients.ToList();
            return View(clients);
        }

        public async Task<IActionResult> ClientsListWithSearch(string Surname, string Name)
        {

            var clients = from m in _context.clients select m;

            if (!String.IsNullOrEmpty(Surname) && !String.IsNullOrEmpty(Name))
            {
                clients = clients.Where(a => a.Surname == Surname && a.Name == Name);
            }


            return View(clients);
        }



        public IActionResult ClientDetail(int id)
        {
            if (id == null || _context.clients == null)
            {
                return NotFound();
            }

            var client = _context.clients.FirstOrDefault(t => t.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            Response.Cookies.Append("clientId", id.ToString());
            return View(client);
        }


        public async Task<IActionResult> InfoClientsBasket()
        {
            //var baskets = from m in _context.baskets select m;

            var baskets = _context.baskets.Where(a => a.ClientId == Convert.ToInt32(Request.Cookies["clientId"]));

            List<int> basketsId = new List<int>();
            
            foreach (Basket basket in baskets)
            {
                basketsId.Add(basket.TourId);
            }


            var tours = _context.tours.Where(a => basketsId.Contains(a.Id));

            return View(tours);
        }

        public async Task<IActionResult> TourFormalization(int id)
        {            
            var tourInBasket = _context.baskets.Where(a => a.TourId == id && a.ClientId == Convert.ToInt32(Request.Cookies["clientId"]));
            
            int tourId = 0;
            foreach (Basket basket in tourInBasket)
            {
                tourId = basket.Id;
            }

            if (tourId > 0)
            {
                var tour = await _context.baskets.FindAsync(tourId);
                if (tour != null)
                {
                    _context.baskets.Remove(tour);
                }
            }

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

    }
}
