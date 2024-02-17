using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;

namespace MvcCineAdoNet.Controllers
{
    public class PeliculasController : Controller
    {
        private IRepositoryCine repo;

        public PeliculasController(IRepositoryCine repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Medio> peliculas = this.repo.GetPeliculas();
            return View(peliculas);
        }
    }
}
