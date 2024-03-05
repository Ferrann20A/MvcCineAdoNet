using Microsoft.EntityFrameworkCore;
using MvcCineAdoNet.Data;
using MvcCineAdoNet.Helpers;
using MvcCineAdoNet.Models;
using MvcCoreCryptography.Helpers;

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

    }
}
