using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;

namespace MvcCineAdoNet.Controllers
{
    public class HomeController : Controller
    {
        RepositoryCineSQLServer repo;

        public HomeController()
        {
            this.repo = new RepositoryCineSQLServer();
        }
        public IActionResult Index()
        {
            ResumenMedios resumen = new ResumenMedios();
            resumen.Peliculas = this.repo.GetPeliculas();
            resumen.Series = this.repo.GetSeries();
            return View(resumen);
        }
    }
}
