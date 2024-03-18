using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCineAdoNet.Data;
using MvcCineAdoNet.Helpers;
using MvcCineAdoNet.Models;
using MvcCoreCryptography.Helpers;
using System.Diagnostics.Metrics;
using System.IO;

#region VIEWS Y PROCEDURES

//ALTER procedure[dbo].[SP_INSERT_SERIE]
//(@titulo nvarchar(100), @creador nvarchar(100),
//@anioEstreno int, @numTemporadas int, @popularidad int,
//@idGenero int, @sinopsis nvarchar(max), @trailer nvarchar(500),
//@imagen nvarchar(255), @IMDB float)
//as
//	declare @nextId int
//	select @nextId = max(idserie) + 1 from serie
//	insert into pelicula values
//	(@nextId, @titulo, @creador, @anioEstreno, @numTemporadas, @popularidad,
//    @idGenero, @sinopsis, @trailer, @imagen, @IMDB);

//ALTER procedure[dbo].[SP_INSERT_PELICULA]
//(@titulo nvarchar(100), @director nvarchar(100),
//@anioEstreno int, @duracion int, @popularidad int,
//@idGenero int, @sinopsis nvarchar(max), @trailer nvarchar(500),
//@imagen nvarchar(255), @IMDB float)
//as
//	declare @nextId int
//	select @nextId = max(idpelicula) +1 from pelicula
//	insert into pelicula values
//	(@nextId, @titulo, @director, @anioEstreno, @duracion, @popularidad,
//    @idGenero, @sinopsis, @trailer, @imagen, @IMDB);

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

//alter view V_ACTORES_PELICULA
//as
//	SELECT pp.idPelicula, A.nombre AS nombre_actor, pp.nombre as nombre_personaje, pp.imagen as imagen_personaje
//	FROM Actor A
//	JOIN Personajes_Pelicula PP ON A.idActor = PP.idActor;
//go

//create procedure SP_ACTORES_PELICULA
//(@idpelicula int)
//as
//	select * from V_ACTORES_PELICULA
//	where idpelicula = @idpelicula
//go


//create view V_ACTORES_SERIE
//as
//	SELECT ps.idSerie, A.nombre AS nombre_actor, ps.nombre as nombre_personaje, ps.imagen as imagen_personaje
//	FROM Actor A
//	JOIN Personajes_Serie PS ON A.idActor = PS.idActor;
//go

//create procedure SP_ACTORES_SERIE
//(@idserie int)
//as
//	select * from V_ACTORES_SERIE
//	where idserie = @idserie
//go

//create view V_COMENTARIOS_PELICULA
//AS
//	SELECT CP.idComentario, CP.idPelicula, U.nombre AS Usuario, CP.fechaComentario, CP.comentario
//	FROM Comentario_Pelicula CP
//	JOIN Usuario U ON CP.idUsuario = U.idUsuario
//GO

//create procedure SP_COMENTARIOS_PELICULA
//(@idpelicula int)
//as
//	select * from V_COMENTARIOS_PELICULA
//	where idpelicula = @idpelicula
//go

//create view V_COMENTARIOS_SERIE
//AS
//	SELECT CS.idComentario, CS.idSerie, U.nombre AS Usuario, CS.fechaComentario, CS.comentario
//	FROM Comentario_Serie CS
//	JOIN Usuario U ON CS.idUsuario = U.idUsuario
//GO

//create procedure SP_COMENTARIOS_SERIE
//(@idserie int)
//as
//	select * from V_COMENTARIOS_SERIE
//	where idserie = @idserie
//go

//create procedure SP_INSERT_COMENTARIO_PELICULA
//(@idusuario int, @idpelicula int,
//@fechaComentario datetime, @comentario nvarchar(max))
//as
//	declare @nextId int
//	select @nextId = max(idcomentario) + 1 from Comentario_Pelicula
//	insert into Comentario_Pelicula values(@nextId, @idusuario, @idpelicula, @fechaComentario, @comentario);
//go

//create procedure SP_INSERT_COMENTARIO_SERIE
//(@idusuario int, @idserie int,
//@fechaComentario datetime, @comentario nvarchar(max))
//as
//	declare @nextId int
//	select @nextId = max(idcomentario) + 1 from Comentario_Serie
//	insert into Comentario_Serie values(@nextId, @idusuario, @idserie, @fechaComentario, @comentario);
//go

//create procedure SP_DELETE_FAVORITO_PELICULA
//(@idfavorito int)
//as
//	delete from Favorito_Pelicula where idfavorito = @idfavorito;
//go

//create procedure SP_DELETE_FAVORITO_SERIE
//(@idfavorito int)
//as
//	delete from Favorito_Serie where idfavorito = @idfavorito;
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

        public async Task<Usuario> RegisterUsuarioAsync(string nombre, string email, string password, string fechaNac)
        {
            Usuario user = new Usuario();
            user.IdUsuario = await this.GetMaxIdUsuarioAsync();
            user.Nombre = nombre;
            user.Email = email;
            user.Rol = "usuario";
            user.Salt = HelperTools.GenerateSalt();
            user.Password = HelperCryptography.EncryptPassword(password, user.Salt);
            user.Contrasenia = password;
            user.FechaNac = fechaNac;
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

        public async Task InsertPeliculaAsync(string titulo, string director, int anioEstreno, int duracion, int popularidad, 
            int idGenero, string sinopsis, string trailer, string imagen, double IMDB)
        {
            string sql = "SP_INSERT_PELICULA @titulo, @director, @anioEstreno, @duracion, @popularidad, @idGenero" +
                ", @sinopsis, @trailer, @imagen, @IMDB";
            SqlParameter pamTitulo = new SqlParameter("@titulo", titulo);
            SqlParameter pamDirector = new SqlParameter("@director", director);
            SqlParameter pamAnioEstreno = new SqlParameter("@anioEstreno", anioEstreno);
            SqlParameter pamDuracion = new SqlParameter("@duracion", duracion);
            SqlParameter pamPopularidad = new SqlParameter("@popularidad", popularidad);
            SqlParameter pamIdGenero = new SqlParameter("@idGenero", idGenero);
            SqlParameter pamSinopsis = new SqlParameter("@sinopsis", sinopsis);
            SqlParameter pamTrailer = new SqlParameter("@trailer", trailer);
            SqlParameter pamImagen = new SqlParameter("@imagen", imagen);
            SqlParameter pamIMDB = new SqlParameter("@IMDB", IMDB);
            this.cineContext.Database.ExecuteSqlRaw(sql, pamTitulo, pamDirector, pamAnioEstreno, pamDuracion, pamPopularidad,
                pamIdGenero, pamSinopsis, pamTrailer, pamImagen, pamIMDB);
        }

        public async Task InsertSerieAsync(string titulo, string creador, int anioEstreno, int numTemporadas, int popularidad, 
            int idGenero, string sinopsis, string trailer, string imagen, double IMDB)
        {
            string sql = "SP_INSERT_SERIE @titulo, @creador, @anioEstreno, @numTemporadas, @popularidad, @idGenero" +
                ", @sinopsis, @trailer, @imagen, @IMDB";
            SqlParameter pamTitulo = new SqlParameter("@titulo", titulo);
            SqlParameter pamDirector = new SqlParameter("@creador", creador);
            SqlParameter pamAnioEstreno = new SqlParameter("@anioEstreno", anioEstreno);
            SqlParameter pamDuracion = new SqlParameter("@numTemporadas", numTemporadas);
            SqlParameter pamPopularidad = new SqlParameter("@popularidad", popularidad);
            SqlParameter pamIdGenero = new SqlParameter("@idGenero", idGenero);
            SqlParameter pamSinopsis = new SqlParameter("@sinopsis", sinopsis);
            SqlParameter pamTrailer = new SqlParameter("@trailer", trailer);
            SqlParameter pamImagen = new SqlParameter("@imagen", imagen);
            SqlParameter pamIMDB = new SqlParameter("@IMDB", IMDB);
            this.cineContext.Database.ExecuteSqlRaw(sql, pamTitulo, pamDirector, pamAnioEstreno, pamDuracion, pamPopularidad,
                pamIdGenero, pamSinopsis, pamTrailer, pamImagen, pamIMDB);
        }

        public async Task<List<Genero>> GetGenerosAsync()
        {
            return await this.cineContext.Generos.ToListAsync();
        }

        public async Task<List<ActoresPelicula>> GetActoresByPeliculaAsync(int idpelicula)
        {
            string sql = "SP_ACTORES_PELICULA @idpelicula";
            SqlParameter pamId = new SqlParameter("@idpelicula", idpelicula);
            var consulta = this.cineContext.ActoresPeliculas.FromSqlRaw(sql, pamId);
            List<ActoresPelicula> actoresPeli = await consulta.ToListAsync();
            return actoresPeli;
        }

        public async Task<List<ActoresSerie>> GetActoresBySerieAsync(int idserie)
        {
            string sql = "SP_ACTORES_SERIE @idserie";
            SqlParameter pamId = new SqlParameter("@idserie", idserie);
            var consulta = this.cineContext.ActoresSeries.FromSqlRaw(sql, pamId);
            List<ActoresSerie> actoresSerie = await consulta.ToListAsync();
            return actoresSerie;
        }

        public async Task<List<Pelicula>> GetPeliculasPopularesAsync()
        {
            var consulta = from datos in this.cineContext.Peliculas
                           where datos.Popularidad >= 94
                           select datos;
            List<Pelicula> pelisPopus = await consulta.ToListAsync();
            return pelisPopus;
        }

        public async Task<List<Serie>> GetSeriesPopularesAsync()
        {
            var consulta = (from datos in this.cineContext.Series
                           where datos.Popularidad >= 94
                           select datos).Take(6);
            List<Serie> serisPopus = await consulta.ToListAsync();
            return serisPopus;
        }

        public int GetMaxIdPelicula()
        {
            return this.cineContext.Peliculas.Max(z => z.IdPelicula);
        }

        public async Task<ViewPeliculaCompleta> GetPeliculaRandomAsync(int idpelicula)
        {
            return await this.cineContext.PeliculasCompletas.FirstOrDefaultAsync(x => x.IdPelicula == idpelicula);
        }

        //METODO PARA HACER UN FILTER POR GENERO
        public Task<Pelicula> GetPeliculasByGenero(int idgenero)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ComentarioPelicula>> GetComentariosPeliculaAsync(int idpelicula)
        {
            string sql = "SP_COMENTARIOS_PELICULA @idpelicula";
            SqlParameter pamId = new SqlParameter("@idpelicula", idpelicula);
            var consulta = this.cineContext.ComentariosPelicula.FromSqlRaw(sql, pamId);
            List<ComentarioPelicula> comentarios = await consulta.ToListAsync();
            return comentarios;
        }

        public async Task<List<ComentarioSerie>> GetComentariosSerieAsync(int idserie)
        {
            string sql = "SP_COMENTARIOS_SERIE @idserie";
            SqlParameter pamId = new SqlParameter("@idserie", idserie);
            var consulta = this.cineContext.ComentariosSerie.FromSqlRaw(sql, pamId);
            List<ComentarioSerie> comentarios = await consulta.ToListAsync();
            return comentarios;
        }

        public async Task InsertComentarioPeliculaAsync(int idusuario, int idpelicula, DateTime fechaComentario, string comentario)
        {
            string sql = "SP_INSERT_COMENTARIO_PELICULA @idusuario, @idpelicula, @fechaComentario, @comentario";
            SqlParameter pamIdUsuario = new SqlParameter("@idusuario", idusuario);
            SqlParameter pamIdPelicula = new SqlParameter("@idpelicula", idpelicula);
            SqlParameter pamFechaComentario = new SqlParameter("@fechaComentario", fechaComentario);
            SqlParameter pamComentario = new SqlParameter("@comentario", comentario);
            this.cineContext.Database.ExecuteSqlRaw(sql, pamIdUsuario, pamIdPelicula, pamFechaComentario, pamComentario);
        }

        public async Task InsertComentarioSerieAsync(int idusuario, int idserie, DateTime fechaComentario, string comentario)
        {
            string sql = "SP_INSERT_COMENTARIO_SERIE @idusuario, @idserie, @fechaComentario, @comentario";
            SqlParameter pamIdUsuario = new SqlParameter("@idusuario", idusuario);
            SqlParameter pamIdSerie = new SqlParameter("@idserie", idserie);
            SqlParameter pamFechaComentario = new SqlParameter("@fechaComentario", fechaComentario);
            SqlParameter pamComentario = new SqlParameter("@comentario", comentario);
            this.cineContext.Database.ExecuteSqlRaw(sql, pamIdUsuario, pamIdSerie, pamFechaComentario, pamComentario);
        }

        public async Task DeleteFavoritoPeliculaAsync(int idfavorito)
        {
            string sql = "SP_DELETE_FAVORITO_PELICULA @idfavorito";
            SqlParameter pamId = new SqlParameter("@idfavorito", idfavorito);
            this.cineContext.Database.ExecuteSqlRaw(sql, pamId);
        }

        public async Task DeleteFavoritoSerieAsync(int idfavorito)
        {
            string sql = "SP_DELETE_FAVORITO_SERIE @idfavorito";
            SqlParameter pamId = new SqlParameter("@idfavorito", idfavorito);

        }
    }
}
