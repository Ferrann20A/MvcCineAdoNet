using Microsoft.EntityFrameworkCore;
using MvcCineAdoNet.Data;
using MvcCineAdoNet.Models;

namespace MvcCineAdoNet.Repositories
{
    public class RepositoryCineBook: IRepositoryCineBook
    {
        private CineBookContext cineContext;

        public RepositoryCineBook(CineBookContext cineContext)
        {
            this.cineContext = cineContext;
        }

        public async Task<List<Pelicula>> GetPeliculas()
        {
            var consulta = from datos in this.cineContext.Peliculas
                           select datos;
            List<Pelicula> peliculas = await consulta.ToListAsync();
            return peliculas;
        }

        public async Task<List<Serie>> GetSeries()
        {
            var consulta = from datos in this.cineContext.Series
                           select datos;
            return await consulta.ToListAsync();
        }
    }
}
