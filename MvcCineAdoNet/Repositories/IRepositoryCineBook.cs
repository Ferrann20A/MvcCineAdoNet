using MvcCineAdoNet.Models;

namespace MvcCineAdoNet.Repositories
{
    public interface IRepositoryCineBook
    {
        Task<List<Pelicula>> GetPeliculas();
        Task<List<Serie>> GetSeries();
        Task<Usuario> RegisterUsuarioAsync(string nombre, string email, string password);
        Task<Usuario> LogInUserAsync(string email, string password);
    }
}
