using MvcCineAdoNet.Models;

namespace MvcCineAdoNet.Repositories
{
    public interface IRepositoryCineBook
    {
        Task<List<Pelicula>> GetPeliculas();
        Task<List<Serie>> GetSeries();
    }
}
