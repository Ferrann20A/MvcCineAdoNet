using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;

namespace MvcCineAdoNet.Controllers
{
    public class HomeController : Controller
    {
        private IRepositoryCineBook repo;

        public HomeController(IRepositoryCineBook repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
