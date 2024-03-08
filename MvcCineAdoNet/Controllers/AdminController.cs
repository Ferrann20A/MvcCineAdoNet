using Microsoft.AspNetCore.Mvc;

namespace MvcCineAdoNet.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
