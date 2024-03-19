using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Extensions;
using MvcCineAdoNet.Filters;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;
using System.Globalization;
using System.Security.Claims;

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
            if (idpelicula != null)
            {
                List<int> idspeliculas;
                if(HttpContext.Session.GetString("idspeliculas") == null)
                {
                    idspeliculas = new List<int>();
                }
                else
                {
                    idspeliculas = HttpContext.Session.GetObject<List<int>>("idspeliculas");
                }
                idspeliculas.Add(idpelicula.Value);
                HttpContext.Session.SetObject("idspeliculas", idspeliculas);
                int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                await this.repo.InsertFavoritoPeliculaAsync(idusuario, idpelicula.Value);
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

        //public async Task<IActionResult> _PartialViewPrueba(int? idpelicula)
        //{
        //    if (idpelicula != null)
        //    {
        //        int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        //        await this.repo.InsertFavoritoPeliculaAsync(idusuario, idpelicula.Value);
        //    }
        //    List<Pelicula> peliculas = await this.repo.GetPeliculasAsync();
        //    return PartialView("_PartialViewPrueba", peliculas);
        //}

        //[HttpPost]
        //public async Task<IActionResult> _PartialViewPrueba(string titulo)
        //{
        //    if (titulo == null)
        //    {
        //        ViewData["mensaje"] = "Debe introducir un título para buscar una película.";
        //        return View();
        //    }
        //    else
        //    {
        //        List<Pelicula> peliculasEncontradas = await this.repo.BuscadorPeliculasAsync(titulo);
        //        return PartialView("_PartialViewPrueba", peliculasEncontradas);
        //    }
        //}

        public async Task<IActionResult> DetailsPelicula(int idpelicula)
        {
            ViewPeliculaCompleta peli = await this.repo.FindPeliculaCompletaAsync(idpelicula);
            List<ActoresPelicula> actoresPeli = await this.repo.GetActoresByPeliculaAsync(idpelicula);
            List<ComentarioPelicula> comentarios = await this.repo.GetComentariosPeliculaAsync(idpelicula);
            ResumenDetailsPelicula resumen = new ResumenDetailsPelicula
            {
                PeliculaCompleta = peli,
                ActoresPelicula = actoresPeli,
                ComentariosPelicula = comentarios
            };
            return View(resumen);
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> CreateComentario(int idpelicula)
        {
            ViewData["idpelicula"] = idpelicula;
            return View();
        }

        [AuthorizeUsuarios]
        [HttpPost]
        public async Task<IActionResult> CreateComentario(string comentario, int idpelicula)
        {
            if(comentario == null)
            {
                ViewData["mensaje"] = "Debes introducir un comentario";
                return View();
            }
            int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            DateTime fechaActual = DateTime.ParseExact(DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"), "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture);
            await this.repo.InsertComentarioPeliculaAsync(idusuario, idpelicula, fechaActual, comentario);
            ViewData["mensaje"] = "Nuevo comentario introducido con éxito!!";
            return RedirectToAction("DetailsPelicula", new {idpelicula = idpelicula});
        }
    }
}
