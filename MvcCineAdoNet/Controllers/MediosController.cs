using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;

namespace MvcCineAdoNet.Controllers
{
    public class MediosController : Controller
    {
        private IRepositoryCine repo;

        public MediosController(IRepositoryCine repo)
        {
            this.repo = repo;
        }

        public IActionResult Index()
        {
            List<Medio> peliculas = this.repo.GetPeliculas();
            return View(peliculas);
        }

        public IActionResult Create()
        {
            ViewData["tiposmedio"] = this.repo.GetTiposMedio();
            ViewData["generos"] = this.repo.GetGeneros();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Medio medio)
        {
            ViewData["tiposmedio"] = this.repo.GetTiposMedio();
            ViewData["generos"] = this.repo.GetGeneros();
            this.repo.InsertMedio(medio.TipoMedioId, medio.Titulo, medio.Director, medio.AnioEstreno, medio.ClasificacionEdad,
                medio.Sinopsis, medio.DuracionMins, medio.PuntuacionMedia, medio.Estado, medio.Imagen, medio.GeneroId);
            return RedirectToAction("Index", "Home");
        }
    }
}
