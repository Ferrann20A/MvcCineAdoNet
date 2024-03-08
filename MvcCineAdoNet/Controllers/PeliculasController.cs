using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;

namespace MvcCineAdoNet.Controllers
{
    public class PeliculasController : Controller
    {
        private IRepositoryCineBook repo;

        public PeliculasController(IRepositoryCineBook repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> BuscadorPeliculas()
        {
            List<Pelicula> peliculas = await this.repo.GetPeliculasAsync();
            return View(peliculas);
        }

        [HttpPost]
        public async Task<IActionResult> BuscadorPeliculas(string titulo)
        {
            List<Pelicula> peliculasEncontradas = await this.repo.BuscadorPeliculasAsync(titulo);
            return View(peliculasEncontradas);
        }
    }
}
