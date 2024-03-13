using MvcCineAdoNet.Models;

namespace MvcCineAdoNet.Repositories
{
    public interface IRepositoryCineBook
    {
        Task<List<Pelicula>> GetPeliculasAsync();
        Task<List<Serie>> GetSeriesAsync();
        Task<Usuario> RegisterUsuarioAsync(string nombre, string email, string password);
        Task<Usuario> LogInUserAsync(string email, string password);
        Task<Usuario> FindUsuarioAsync(int idUsuario);
        Task<List<Pelicula>> BuscadorPeliculasAsync(string titulo);
        Task<List<Serie>> BuscadorSeriesAsync(string titulo);
        Task<Pelicula> FindPeliculaAsync(int idpelicula);
        Task<Serie> FindSerieAsync(int idserie);
        Task InsertFavoritoPeliculaAsync(int idusuario, int idpelicula);
        Task<List<Pelicula>> GetFavoritosPeliculaSessionAsync(List<int> ids);
        Task<List<ViewAllPelicula>> GetFavoritosPeliculaByUsuarioAsync(int idusuario);
        Task<List<ViewPeliculaCompleta>> GetPeliculasCompletasAsync();
        Task<ViewPeliculaCompleta> FindPeliculaCompletaAsync(int idpelicula);
        Task<List<ViewSerieCompleta>> GetSeriesCompletasAsync();
        Task<ViewSerieCompleta> FindSerieCompletaAsync(int idserie);
        Task InsertFavoritoSerieAsync(int idusuario, int idserie);
        Task<List<ViewAllSerie>> GetFavoritosSerieByUsuarioAsync(int idusuario);
    }
}
