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
            List<Pelicula> peliculas = await this.repo.GetPeliculasAsync();
            return View(peliculas);
        }

        public async Task<IActionResult> Prueba()
        {
            List<Serie> series = await this.repo.GetSeriesAsync();
            return View(series);
        }

        //PRUEBA
        public async Task<IActionResult> PerfilUsuarioPrueba(int idusuario)
        {
            Usuario user = await this.repo.FindUsuarioAsync(idusuario);
            return View(user);
        }

        public IActionResult PerfilPrueba()
        {
            return View();
        }

    }
}
