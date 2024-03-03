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

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> IndexPrueba()
        {
            List<Pelicula> peliculas = await this.repo.GetPeliculas();
            return View(peliculas);
        }

        public async Task<IActionResult> Prueba()
        {
            List<Serie> series = await this.repo.GetSeries();
            return View(series);
        }
    }
}
