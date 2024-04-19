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
            //LO QUE TENGO QUE HACER AQUI ES UTILIZAR EL METODO DE FindPeliculaCompletaAsync
            //QUE ES EL MISMO METODO QUE EL DE GetPeliculaRandomAsync ASI ME AHORRO ESE METODO
            //QUE ES INNECESARIO
            ViewPeliculaCompleta peli = await this.repo.FindPeliculaCompletaAsync(idRandom);
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
