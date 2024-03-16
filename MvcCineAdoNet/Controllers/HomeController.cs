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

        /*
         Para el home quiero un div principal (estara dividido en dos, en la parte de la izquierda ira la peli y en 
        la de la derecha una pequña animacion) donde se cargara entre todas las peliculas que hay una aleatoria
        cada vez que se inicia la applicacion y en el div habra una imagen panoramica de la pelicula y el titulo
        con la sinopsis y el año, genero y duracion.
        Despues mas abajo habra otra seccion donde se carganran las peliculas mas populares y otra seccion
        donde se cargaran las series mas populares.*/
    }
}
