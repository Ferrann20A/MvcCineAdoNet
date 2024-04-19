using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;

namespace MvcCineAdoNet.Services
{
    public class ServiceCineBook : IRepositoryCineBook
    {
        public Task<List<Pelicula>> BuscadorPeliculasAsync(string titulo)
        {
            throw new NotImplementedException();
        }

        public Task<List<Serie>> BuscadorSeriesAsync(string titulo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFavoritoPeliculaAsync(int idfavorito)
        {
            throw new NotImplementedException();
        }

        public Task DeleteFavoritoSerieAsync(int idfavorito)
        {
            throw new NotImplementedException();
        }

        public Task<Pelicula> FindPeliculaAsync(int idpelicula)
        {
            throw new NotImplementedException();
        }

        public Task<ViewPeliculaCompleta> FindPeliculaCompletaAsync(int idpelicula)
        {
            throw new NotImplementedException();
        }

        public Task<Serie> FindSerieAsync(int idserie)
        {
            throw new NotImplementedException();
        }

        public Task<ViewSerieCompleta> FindSerieCompletaAsync(int idserie)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> FindUsuarioAsync(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<ActoresPelicula>> GetActoresByPeliculaAsync(int idpelicula)
        {
            throw new NotImplementedException();
        }

        public Task<List<ActoresSerie>> GetActoresBySerieAsync(int idserie)
        {
            throw new NotImplementedException();
        }

        public Task<List<ComentarioPelicula>> GetComentariosPeliculaAsync(int idpelicula)
        {
            throw new NotImplementedException();
        }

        public Task<List<ComentarioSerie>> GetComentariosSerieAsync(int idserie)
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewAllPelicula>> GetFavoritosPeliculaByUsuarioAsync(int idusuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pelicula>> GetFavoritosPeliculaSessionAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewAllSerie>> GetFavoritosSerieByUsuarioAsync(int idusuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<Genero>> GetGenerosAsync()
        {
            throw new NotImplementedException();
        }

        public int GetMaxIdPelicula()
        {
            throw new NotImplementedException();
        }

        public Task<ViewPeliculaCompleta> GetPeliculaRandomAsync(int idpelicula)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pelicula>> GetPeliculasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Pelicula> GetPeliculasByGenero(int idgenero)
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewPeliculaCompleta>> GetPeliculasCompletasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Pelicula>> GetPeliculasPopularesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Serie>> GetSeriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewSerieCompleta>> GetSeriesCompletasAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Serie>> GetSeriesPopularesAsync()
        {
            throw new NotImplementedException();
        }

        public Task InsertComentarioPeliculaAsync(int idusuario, int idpelicula, DateTime fechaComentario, string comentario)
        {
            throw new NotImplementedException();
        }

        public Task InsertComentarioSerieAsync(int idusuario, int idserie, DateTime fechaComentario, string comentario)
        {
            throw new NotImplementedException();
        }

        public Task InsertFavoritoPeliculaAsync(int idusuario, int idpelicula)
        {
            throw new NotImplementedException();
        }

        public Task InsertFavoritoSerieAsync(int idusuario, int idserie)
        {
            throw new NotImplementedException();
        }

        public Task InsertPeliculaAsync(string titulo, string director, int anioEstreno, int duracion, int popularidad, int idGenero, string sinopsis, string trailer, string imagen, double IMDB)
        {
            throw new NotImplementedException();
        }

        public Task InsertSerieAsync(string titulo, string creador, int anioEstreno, int numTemporadas, int popularidad, int idGenero, string sinopsis, string trailer, string imagen, double IMDB)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> LogInUserAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario> RegisterUsuarioAsync(string nombre, string email, string password, string fechaNac)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUsuarioAsync(int idusuario, string nombre, string email, string fechaNac)
        {
            throw new NotImplementedException();
        }
    }
}
