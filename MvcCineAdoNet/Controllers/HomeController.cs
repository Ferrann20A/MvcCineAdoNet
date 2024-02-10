using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;

namespace MvcCineAdoNet.Controllers
{
    public class HomeController : Controller
    {
        RepositoryCine repo;

        public HomeController()
        {
            this.repo = new RepositoryCine();
        }
        public IActionResult Index()
        {
            List<Medio> peliculas = this.repo.GetPeliculas();
            return View(peliculas);
        }

        public IActionResult Prueba()
        {
            return View();
        }
    }
}
