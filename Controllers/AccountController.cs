using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TravelAgency_Prod.Controllers;
using TravelAgency_Prod.Data;
using TravelAgency_Prod.Models;

namespace TravelAgency_Prod.Controllers
{
    public class AccountController : Controller
    {
        private readonly TravelAgencyContext _context;
        public AccountController(TravelAgencyContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> EmailValidation([Bind("Email")] Client client)
        {
            if (_context.clients.Any(x => x.Email == client.Email))
            {
                return Json(false);
            }
            return Json(true);

        }


        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Logout()
        {

            return Redirect("~/Home/Index");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("Id,Surname,Name,Patronymic,Email,Password, Pasport,ZagranPasport,Phone,Visa")] Client client)
        {
            _context.Add(client);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Clients", new { Id = client.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] ForLogin forlogin)
        {
            Client client = new Client();
            TourManager tourManager = new TourManager();
            Administrator administrator = new Administrator();
            client.Email = forlogin.Email;
            tourManager.Email = forlogin.Email;
            administrator.Email = forlogin.Email;
            client.Password = forlogin.Password;
            tourManager.Password = forlogin.Password;
            administrator.Password = forlogin.Password;

            if (_context.clients.Any(x => x.Email == client.Email && x.Password == client.Password))
            {
                int id = _context.clients.Where(c => c.Email == client.Email && c.Password == client.Password)
                    .Select(c => c.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("Index", "Clients", new { Id = id });
            }
            else if (_context.tourManagers.Any(x => x.Email == tourManager.Email && x.Password == tourManager.Password))
            {
                int id = _context.tourManagers.Where(c => c.Email == tourManager.Email && c.Password == tourManager.Password)
                    .Select(c => c.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("ToursListForManager", "TourManagers", new { Id = id });
            }
            else if (_context.administrators.Any(x => x.Email == administrator.Email && x.Password == administrator.Password))
            {
                int id = _context.administrators.Where(c => c.Email == administrator.Email && c.Password == administrator.Password)
                    .Select(c => c.Id).FirstOrDefault();
                Response.Cookies.Append("userId", id.ToString());
                return RedirectToAction("Index", "Admin", new { Id = id });
            }
            else
            {
                return NotFound();
            }
        }


    }
}