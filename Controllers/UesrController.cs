using Microsoft.AspNetCore.Mvc;

namespace TravelAgency_Prod.Controllers
{
    public class UesrController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
