using MvcCineAdoNet.Models;

namespace MvcCineAdoNet.Repositories
{
    public interface IRepositoryCineBook
    {
        Task<List<Pelicula>> GetPeliculasAsync();
        Task<List<Serie>> GetSeriesAsync();
        Task<Usuario> RegisterUsuarioAsync(string nombre, string email, string password, string fechaNac);
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
        Task InsertPeliculaAsync(string titulo, string director, int anioEstreno, int duracion, int popularidad,
                                   int idGenero, string sinopsis, string trailer, string imagen, double IMDB);
        Task InsertSerieAsync(string titulo, string creador, int anioEstreno, int numTemporadas, int popularidad,
                                   int idGenero, string sinopsis, string trailer, string imagen, double IMDB);
        Task<List<Genero>> GetGenerosAsync();
        Task<List<ActoresPelicula>> GetActoresByPeliculaAsync(int idpelicula);
        Task<List<ActoresSerie>> GetActoresBySerieAsync(int idserie);
        Task<List<Pelicula>> GetPeliculasPopularesAsync();
        Task<List<Serie>> GetSeriesPopularesAsync();
        int GetMaxIdPelicula();
        Task<ViewPeliculaCompleta> GetPeliculaRandomAsync(int idpelicula);
        Task<Pelicula> GetPeliculasByGenero(int idgenero);
        Task<List<ComentarioPelicula>> GetComentariosPeliculaAsync(int idpelicula);
        Task<List<ComentarioSerie>> GetComentariosSerieAsync(int idserie);
        Task InsertComentarioPeliculaAsync(int idusuario, int idpelicula, DateTime fechaComentario, string comentario);
        Task InsertComentarioSerieAsync(int idusuario, int idserie, DateTime fechaComentario, string comentario);
    }
}
