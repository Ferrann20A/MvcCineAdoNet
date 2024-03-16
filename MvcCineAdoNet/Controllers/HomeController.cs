using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;

namespace MvcCineAdoNet.Controllers
{
    public class HomeController : Controller
    {
        private IRepositoryCineBook repo;

        public HomeController(IRepositoryCineBook repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            int maxIdPelicula = this.repo.GetMaxIdPelicula();
            Random random = new Random();
            int idRandom = random.Next(1, maxIdPelicula + 1);
            ViewPeliculaCompleta peli = await this.repo.GetPeliculaRandomAsync(idRandom);
            List<Pelicula> pelisPopus = await this.repo.GetPeliculasPopularesAsync();
            List<Serie> seriesPopus = await this.repo.GetSeriesPopularesAsync();
            ResumenHome resumen = new ResumenHome
            {
                PeliculaRandom = peli,
                PeliculasPopulares = pelisPopus,
                SeriesPopulares = seriesPopus
            };
            return View(resumen);
        }
    }
}
