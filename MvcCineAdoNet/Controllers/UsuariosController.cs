using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;
using MvcCineAdoNet.Extensions;

namespace MvcCineAdoNet.Controllers
{
    public class UsuariosController : Controller
    {
        private IRepositoryCineBook repo;
        public UsuariosController(IRepositoryCineBook repo)
        {
            this.repo = repo;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string nombre, string email, string password)
        {
            Usuario user = await this.repo.RegisterUsuarioAsync(nombre, email, password);
            return RedirectToAction("LogIn");
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            Usuario user = await this.repo.LogInUserAsync(email, password);
            if (user == null)
            {
                ViewData["mensaje_error"] = "Credenciales incorrectas";
                return View();
            }
            else
            {
                HttpContext.Session.SetObject("usuario", user);
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("usuario");
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> PerfilUsuario(int idUsuario)
        {
            Usuario user = await this.repo.FindUsuarioAsync(idUsuario);
            return View(user);
        }

        public IActionResult Perfil()
        {
            return View();
        }

        public async Task<IActionResult> MiListaUsuario(int? ideliminar)
        {
            //List<int> idspeliculas = HttpContext.Session.GetObject<List<int>>("idspeliculas");
            //if(idspeliculas != null)
            //{
            //    if(ideliminar != null)
            //    {
            //        idspeliculas.Remove(ideliminar.Value);
            //        if(idspeliculas.Count() == 0)
            //        {
            //            HttpContext.Session.Remove("idspeliculas");
            //        }
            //        else
            //        {
            //            HttpContext.Session.SetObject("idspeliculas", idspeliculas);
            //        }
            //    }
            //    //List<Pelicula> peliculas = await this.repo.GetFavoritosPeliculaSessionAsync(idspeliculas);
            //}
            Usuario user = HttpContext.Session.GetObject<Usuario>("usuario");
            if(user != null)
            {

            List<ViewAllPelicula> pelisFav = await this.repo.GetFavoritosPeliculaByUsuarioAsync(user.IdUsuario);
            List<ViewAllSerie> seriesFav = await this.repo.GetFavoritosSerieByUsuarioAsync(user.IdUsuario);
            ResumenMiListaPeliculasSeries resumen = new ResumenMiListaPeliculasSeries
            {
                ViewAllPeliculas = pelisFav,
                ViewAllSeries = seriesFav
            };
            return View(resumen);
            }
            return View();
            //return View();
        }
    }
}
