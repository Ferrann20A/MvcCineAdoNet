using Microsoft.AspNetCore.Mvc;

namespace MvcCineAdoNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Prueba()
        {
            return View();
        }
    }
}
