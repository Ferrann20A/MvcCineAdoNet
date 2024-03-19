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
        public async Task<IActionResult> Register(string nombre, string email, string password, string fechaNac)
        {
            await this.repo.RegisterUsuarioAsync(nombre, email, password, fechaNac);
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
        public async Task<IActionResult> PerfilUsuario(int idusuario)
        {
            Usuario user = await this.repo.FindUsuarioAsync(idusuario);
            return View(user);
        }

        [AuthorizeUsuarios]
        public IActionResult Perfil()
        {
            return View();
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> MiListaUsuario(int? ideliminarpelicula, int? idpelicula,
            int? ideliminarserie, int? idserie)
        {
            if(HttpContext.User.Identity.IsAuthenticated == true)
            {
                List<int> idspeliculas = HttpContext.Session.GetObject<List<int>>("idspeliculas");
                List<int> idsseries = HttpContext.Session.GetObject<List<int>>("idsseries");
                if(idspeliculas != null)
                {
                    if(idpelicula != null)
                    {
                        idspeliculas.Remove(idpelicula.Value);
                        if(idspeliculas.Count() == 0)
                        {
                            HttpContext.Session.Remove("idspeliculas");
                        }
                        else
                        {
                            HttpContext.Session.SetObject("idspeliculas", idspeliculas);
                        }
                    }
                }
                if (idsseries != null)
                {
                    if (idserie != null)
                    {
                        idsseries.Remove(idserie.Value);
                        if (idsseries.Count() == 0)
                        {
                            HttpContext.Session.Remove("idsseries");
                        }
                        else
                        {
                            HttpContext.Session.SetObject("idsseries", idsseries);
                        }
                    }
                }
                if (ideliminarpelicula != null)
                {
                    await this.repo.DeleteFavoritoPeliculaAsync(ideliminarpelicula.Value);
                }
                if(ideliminarserie != null)
                {
                    await this.repo.DeleteFavoritoSerieAsync(ideliminarserie.Value);
                }
                int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                List<ViewAllPelicula> pelisFav = await this.repo.GetFavoritosPeliculaByUsuarioAsync(idusuario);
                List<ViewAllSerie> seriesFav = await this.repo.GetFavoritosSerieByUsuarioAsync(idusuario);
                ResumenMiListaPeliculasSeries resumen = new ResumenMiListaPeliculasSeries
                {
                    ViewAllPeliculas = pelisFav,
                    ViewAllSeries = seriesFav
                };
                return View(resumen);
            }
            return View();
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> EditarPerfil(int idusuario)
        {
            Usuario user = await this.repo.FindUsuarioAsync(idusuario);
            return View(user);
        }

        [AuthorizeUsuarios]
        [HttpPost]
        public async Task<IActionResult> EditarPerfil(int idusuario, string nombre, string email, string fechaNac)
        {
            await this.repo.UpdateUsuarioAsync(idusuario, nombre, email, fechaNac);
            return RedirectToAction("PerfilUsuario", new { idusuario = idusuario });
        }
    }
}
