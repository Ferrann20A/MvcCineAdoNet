using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> BuscadorSeries()
        {
            List<Serie> series = await this.repo.GetSeriesAsync();
            return View(series);
        }

        [HttpPost]
        public async Task<IActionResult> BuscadorSeries(string titulo)
        {
            List<Serie> seriesEncontradas = await this.repo.BuscadorSeriesAsync(titulo);
            return View(seriesEncontradas);
        }
    }
}
