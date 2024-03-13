using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Extensions;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;

namespace MvcCineAdoNet.Controllers
{
    public class SeriesController : Controller
    {
        private IRepositoryCineBook repo;

        public SeriesController(IRepositoryCineBook repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> BuscadorSeries(int? idserie)
        {
            if(idserie != null)
            {
                Usuario user = HttpContext.Session.GetObject<Usuario>("usuario");
                await this.repo.InsertFavoritoSerieAsync(user.IdUsuario, idserie.Value);
            }
            List<Serie> series = await this.repo.GetSeriesAsync();
            return View(series);
        }

        [HttpPost]
        public async Task<IActionResult> BuscadorSeries(string titulo)
        {
            List<Serie> seriesEncontradas = await this.repo.BuscadorSeriesAsync(titulo);
            return View(seriesEncontradas);
        }

        public async Task<IActionResult> DetailsSerie(int idserie)
        {
            ViewSerieCompleta serie = await this.repo.FindSerieCompletaAsync(idserie);
            return View(serie);
        }
    }
}
