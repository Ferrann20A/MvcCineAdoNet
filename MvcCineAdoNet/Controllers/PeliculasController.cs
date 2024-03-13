using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Extensions;
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

        public async Task<IActionResult> BuscadorPeliculas(int? idpelicula)
        {
            if(idpelicula != null)
            {
                //List<int> idspeliculas;
                //if(HttpContext.Session.GetString("idspeliculas") == null)
                //{
                //    idspeliculas = new List<int>();
                //}
                //else
                //{
                //    idspeliculas = HttpContext.Session.GetObject<List<int>>("idspeliculas");
                //}
                //idspeliculas.Add(idpelicula.Value);
                //HttpContext.Session.SetObject("idspeliculas", idspeliculas);
                Usuario user = HttpContext.Session.GetObject<Usuario>("usuario");
                await this.repo.InsertFavoritoPeliculaAsync(user.IdUsuario, idpelicula.Value);
            }
            List<Pelicula> peliculas = await this.repo.GetPeliculasAsync();
            return View(peliculas);
        }

        [HttpPost]
        public async Task<IActionResult> BuscadorPeliculas(string titulo)
        {
            if(titulo == null)
            {
                ViewData["mensaje"] = "Debe introducir un título para buscar una película.";
                return View();
            }
            else
            {
                List<Pelicula> peliculasEncontradas = await this.repo.BuscadorPeliculasAsync(titulo);
                return View(peliculasEncontradas);
            }
        }

        public async Task<IActionResult> DetailsPelicula(int idpelicula)
        {
            ViewPeliculaCompleta peli = await this.repo.FindPeliculaCompletaAsync(idpelicula);
            return View(peli);
        }
    }
}
