using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;

namespace MvcCineAdoNet.Controllers
{
    public class HomeController : Controller
    {
        RepositoryCineSQLServer repo;

        public HomeController()
        {
            this.repo = new RepositoryCineSQLServer();
        }
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
