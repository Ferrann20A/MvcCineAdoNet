using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;
using MvcCineAdoNet.Extensions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MvcCineAdoNet.Filters;

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
                ClaimsIdentity identity = new ClaimsIdentity(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    ClaimTypes.Name, ClaimTypes.Role);
                Claim claimId = new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString());
                identity.AddClaim(claimId);
                Claim claimNombre = new Claim(ClaimTypes.Name, user.Nombre);
                identity.AddClaim(claimNombre);
                Claim claimEmail = new Claim("Email", user.Email);
                identity.AddClaim(claimEmail);
                Claim claimRol = new Claim(ClaimTypes.Role, user.Rol);
                identity.AddClaim(claimRol);
                Claim claimContrasenia = new Claim("Contrasenia", user.Contrasenia);
                identity.AddClaim(claimContrasenia);
                if(user.Rol == "admin")
                {
                    identity.AddClaim(new Claim("Admin", "Soy el admin"));
                }
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);
                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();

                HttpContext.Session.SetObject("usuario", user);
                return RedirectToAction(action, controller);
            }
        }

        public async  Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("usuario");
            return RedirectToAction("Index", "Home");
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> PerfilUsuario(int idUsuario)
        {
            Usuario user = await this.repo.FindUsuarioAsync(idUsuario);
            return View(user);
        }

        [AuthorizeUsuarios]
        public IActionResult Perfil()
        {
            return View();
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> MiListaUsuario(int? ideliminar)
        {
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
        }
    }
}
