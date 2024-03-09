using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCineAdoNet.Data;
using MvcCineAdoNet.Helpers;
using MvcCineAdoNet.Models;
using MvcCoreCryptography.Helpers;

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

        public async Task<List<Serie>> GetSeriesAsync()
        {
            var consulta = from datos in this.cineContext.Series
                           select datos;
            return await consulta.ToListAsync();
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

        
    }
}
