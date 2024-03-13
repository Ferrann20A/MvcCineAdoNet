using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCineAdoNet.Data;
using MvcCineAdoNet.Helpers;
using MvcCineAdoNet.Models;
using MvcCoreCryptography.Helpers;
using System.Diagnostics.Metrics;

#region VIEWS Y PROCEDURES

//create procedure SP_BUSCADOR_PELICULAS
//(@titulo nvarchar(100))
//as
//	select *
//	from pelicula
//	where titulo like '%' + @titulo + '%'
//go


//create procedure SP_BUSCADOR_SERIES
//(@titulo nvarchar(100))
//as
//	select *
//	from serie
//	where titulo like '%' + @titulo + '%'
//go

//create procedure SP_FIND_PELICULA
//(@idpelicula int)
//as
//	select *
//	from pelicula
//	where idpelicula = @idpelicula
//go

//create procedure SP_FIND_SERIE
//(@idserie int)
//as
//	select *
//	from serie
//	where idserie = @idserie
//go

//create procedure SP_INSERT_FAVORITO_PELICULA
//(@idusuario int, @idpelicula int)
//as
//	declare @nextId int
//	select @nextId = max(idfavorito) + 1 from favorito_pelicula
//	insert into Favorito_Pelicula values (@nextId, @idusuario, @idpelicula)
//go

//create procedure SP_FAVORITOS_PELICULA_USUARIO
//(@idusuario int)
//as
//	select fp.idFavorito, fp.idUsuario, p.idpelicula, p.titulo, p.director,
//    p.anioEstreno, p.duracion, g.nombre as genero, p.sinopsis, p.trailer, p.imagen, p.IMDB
//	from favorito_pelicula fp, usuario u, pelicula p, genero g
//	where fp.idusuario = u.idusuario
//	and fp.idpelicula = p.idpelicula
//	and p.idgenero = g.idgenero
//	and fp.idusuario = @idusuario
//go

//alter view V_ALL_PELICULA
//as
//	select fp.idFavorito, fp.idUsuario, p.idpelicula, p.titulo, p.director,
//    p.anioEstreno, p.duracion, p.popularidad , g.nombre as genero, p.sinopsis, p.trailer, p.imagen, p.IMDB
//	from favorito_pelicula fp, usuario u, pelicula p, genero g
//	where fp.idusuario = u.idusuario
//	and fp.idpelicula = p.idpelicula
//	and p.idgenero = g.idgenero
//go

//create procedure SP_INSERT_FAVORITO_SERIE
//(@idusuario int, @idserie int)
//as
//	declare @nextId int
//	select @nextId = max(idfavorito) + 1 from favorito_serie
//	insert into Favorito_Serie values (@nextId, @idusuario, @idserie)
//go

//create procedure SP_FAVORITOS_SERIE_USUARIO
//(@idusuario int)
//as
//	select fs.idFavorito, fs.idUsuario, s.idserie, s.titulo, s.creador,
//    s.anioEstreno, s.numTemporadas, s.popularidad , g.nombre as genero, s.sinopsis, s.trailer, s.imagen, s.IMDB
//	from favorito_serie fs, usuario u, serie s, genero g
//	where fs.idusuario = u.idusuario
//	and fs.idserie = s.idserie
//	and s.idgenero = g.idgenero
//	and fs.idusuario = @idusuario
//go

//create view V_ALL_SERIE
//as
//	select fs.idFavorito, fs.idUsuario, s.idserie, s.titulo, s.creador,
//    s.anioEstreno, s.numTemporadas, s.popularidad , g.nombre as genero, s.sinopsis, s.trailer, s.imagen, s.IMDB
//	from favorito_serie fs, usuario u, serie s, genero g
//	where fs.idusuario = u.idusuario
//	and fs.idserie = s.idserie
//	and s.idgenero = g.idgenero
//go

//create view V_PELICULA_COMPLETA
//AS
//	select p.idPelicula, p.titulo, p.director, p.anioEstreno, p.duracion, p.popularidad,
//    g.nombre as genero, p.sinopsis, p.trailer, p.imagen, p.IMDB
//	from pelicula p join genero g
//	on p.idgenero = g.idgenero
//GO

//create view V_SERIE_COMPLETA
//AS
//	select s.idSerie, s.titulo, s.creador, s.anioEstreno, s.numTemporadas, s.popularidad,
//    g.nombre as genero, s.sinopsis, s.trailer, s.imagen, s.IMDB
//	from serie s join genero g
//	on s.idgenero = g.idgenero
//GO

//alter procedure SP_REPARTO_PELICULA
//(@idpelicula int)
//as
//	SELECT A.nombre AS nombre_actor, pp.nombre as nombre_personaje, pp.imagen as imagen_personaje
//	FROM Actor A
//	JOIN Personajes_Pelicula PP ON A.idActor = PP.idActor
//	WHERE PP.idPelicula = @idpelicula;
//go

#endregion

namespace MvcCineAdoNet.Repositories
{
    public class RepositoryCineBook: IRepositoryCineBook
    {
        private CineBookContext cineContext;

        public RepositoryCineBook(CineBookContext cineContext)
        {
            this.cineContext = cineContext;
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            var consulta = from datos in this.cineContext.Peliculas
                           select datos;
            List<Pelicula> peliculas = await consulta.ToListAsync();
            return peliculas;
        }

        public async Task<List<ViewPeliculaCompleta>> GetPeliculasCompletasAsync()
        {
            var peliculasCompletas = await this.cineContext.PeliculasCompletas.ToListAsync();
            return peliculasCompletas;
        }

        public async Task<ViewPeliculaCompleta> FindPeliculaCompletaAsync(int idpelicula)
        {
            return await this.cineContext.PeliculasCompletas.FirstOrDefaultAsync(x => x.IdPelicula == idpelicula);
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            var consulta = from datos in this.cineContext.Series
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<List<ViewSerieCompleta>> GetSeriesCompletasAsync()
        {
            var seriesCompletas = await this.cineContext.SeriesCompletas.ToListAsync();
            return seriesCompletas;
        }

        public async Task<ViewSerieCompleta> FindSerieCompletaAsync(int idserie)
        {
            return await this.cineContext.SeriesCompletas.FirstOrDefaultAsync(x => x.IdSerie == idserie);
        }

        private async Task<int> GetMaxIdUsuarioAsync()
        {
            if(this.cineContext.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.cineContext.Usuarios.MaxAsync(x => x.IdUsuario) + 1;
            }
        }

        public async Task<Usuario> RegisterUsuarioAsync(string nombre, string email, string password)
        {
            Usuario user = new Usuario();
            user.IdUsuario = await this.GetMaxIdUsuarioAsync();
            user.Nombre = nombre;
            user.Email = email;
            user.Rol = "usuario";
            user.Salt = HelperTools.GenerateSalt();
            user.Password = HelperCryptography.EncryptPassword(password, user.Salt);
            user.Contrasenia = password;
            this.cineContext.Usuarios.Add(user);
            await this.cineContext.SaveChangesAsync();
            return user;
        }

        public async Task<Usuario> LogInUserAsync(string email, string password)
        {
            Usuario user = await this.cineContext.Usuarios.FirstOrDefaultAsync(z => z.Email == email);
            if (user == null)
            {
                return null;
            }
            else
            {
                string salt = user.Salt;
                byte[] temp = HelperCryptography.EncryptPassword(password, salt);
                byte[] passUser = user.Password;
                bool response = HelperTools.CompareArrays(temp, passUser);
                if (response == true)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<Usuario> FindUsuarioAsync(int idUsuario)
        {
            var empleado = await this.cineContext.Usuarios.FirstOrDefaultAsync(z => z.IdUsuario == idUsuario);
            return empleado;
        }

        public async Task<List<Pelicula>> BuscadorPeliculasAsync(string titulo)
        {
            string sql = "SP_BUSCADOR_PELICULAS @titulo";
            SqlParameter pamTitulo = new SqlParameter("@titulo", titulo);
            var consulta = this.cineContext.Peliculas.FromSqlRaw(sql, pamTitulo);
            List<Pelicula> peliculas = await consulta.ToListAsync();
            return peliculas;
        }

        public async Task<List<Serie>> BuscadorSeriesAsync(string titulo)
        {
            string sql = "SP_BUSCADOR_SERIES @titulo";
            SqlParameter pamTitulo = new SqlParameter("@titulo", titulo);
            var consulta = this.cineContext.Series.FromSqlRaw(sql, pamTitulo);
            List<Serie> series = await consulta.ToListAsync();
            return series;
        }

        public async Task<Pelicula> FindPeliculaAsync(int idpelicula)
        {
            return await this.cineContext.Peliculas.FirstOrDefaultAsync(x => x.IdPelicula == idpelicula);
        }

        public async Task<Serie> FindSerieAsync(int idserie)
        {
            return await this.cineContext.Series.FirstOrDefaultAsync(z => z.IdSerie == idserie);
        }
        public async Task<List<Pelicula>> GetFavoritosPeliculaSessionAsync(List<int> ids)
        {
            var consulta = from datos in this.cineContext.Peliculas
                           where ids.Contains(datos.IdPelicula)
                           select datos;
            if(consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                return await consulta.ToListAsync();
            }
        }

        public async Task InsertFavoritoPeliculaAsync(int idusuario, int idpelicula)
        {
            string sql = "SP_INSERT_FAVORITO_PELICULA @idusuario, @idpelicula";
            SqlParameter pamIdUsuario = new SqlParameter("@idusuario", idusuario);
            SqlParameter pamIdPelicula = new SqlParameter("@idpelicula", idpelicula);
            this.cineContext.Database.ExecuteSqlRaw(sql, pamIdUsuario, pamIdPelicula);
        }

        public async Task<List<ViewAllPelicula>> GetFavoritosPeliculaByUsuarioAsync(int idusuario)
        {
            string sql = "SP_FAVORITOS_PELICULA_USUARIO @idusuario";
            SqlParameter pamIdusuario = new SqlParameter("@idusuario", idusuario);
            var consulta = this.cineContext.ViewAllPeliculas.FromSqlRaw(sql, pamIdusuario);
            List<ViewAllPelicula> peliculasFav = await consulta.ToListAsync();
            return peliculasFav;
        }

        public async Task InsertFavoritoSerieAsync(int idusuario, int idserie)
        {
            string sql = "SP_INSERT_FAVORITO_SERIE @idusuario, @idserie";
            SqlParameter pamIdUsuario = new SqlParameter("@idusuario", idusuario);
            SqlParameter pamIdSerie = new SqlParameter("@idserie", idserie);
            this.cineContext.Database.ExecuteSqlRaw(sql, pamIdUsuario, pamIdSerie);
        }

        public async Task<List<ViewAllSerie>> GetFavoritosSerieByUsuarioAsync(int idusuario)
        {
            string sql = "SP_FAVORITOS_SERIE_USUARIO @idusuario";
            SqlParameter pamIdusuario = new SqlParameter("@idusuario", idusuario);
            var consulta = this.cineContext.ViewAllSeries.FromSqlRaw(sql, pamIdusuario);
            List<ViewAllSerie> seriesFav = await consulta.ToListAsync();
            return seriesFav;
        }
    }
}
