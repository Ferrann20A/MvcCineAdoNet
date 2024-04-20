using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MvcCineAdoNet.Models;
using MvcCineAdoNet.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace MvcCineAdoNet.Services
{
    public class ServiceCineBook : IRepositoryCineBook
    {
        private string UrlCineBook;
        private MediaTypeWithQualityHeaderValue header;
        private IHttpContextAccessor contextAccessor;

        public ServiceCineBook(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            this.UrlCineBook = configuration.GetValue<string>("ApiUrls:ApiCineBook");
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.contextAccessor = contextAccessor;
        }

        public async Task<string> GetTokenAsync(string email, string password)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/auth/login";
                client.BaseAddress = new Uri(this.UrlCineBook);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                LoginModel model = new LoginModel
                {
                    Email = email,
                    Password = password
                };
                string jsonData = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    JObject keys = JObject.Parse(data);
                    string token = keys.GetValue("token").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlCineBook);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        private async Task<T> CallApiAsync<T>(string request, string token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlCineBook);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        private string GetTokenUser()
        {
            string token = this.contextAccessor.HttpContext.User.FindFirst(x => x.Type == "token").Value;
            return token;
        }

        public async Task<List<Pelicula>> BuscadorPeliculasAsync(string titulo)
        {
            string request = "api/peliculas/buscadorpeliculas/" + titulo;
            List<Pelicula> peliculas = await this.CallApiAsync<List<Pelicula>>(request);
            return peliculas;
        }

        public async Task<List<Serie>> BuscadorSeriesAsync(string titulo)
        {
            string request = "api/series/buscadorseries/" + titulo;
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task DeleteFavoritoPeliculaAsync(int idfavorito)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/favoritos/deletepeliculafavoritos/" + idfavorito;
                client.BaseAddress = new Uri(this.UrlCineBook);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        public async Task DeleteFavoritoSerieAsync(int idfavorito)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/favoritos/deleteseriefavoritos/" + idfavorito;
                client.BaseAddress = new Uri(this.UrlCineBook);
                client.DefaultRequestHeaders.Clear();
                HttpResponseMessage response = await client.DeleteAsync(request);
            }
        }

        public async Task<Pelicula> FindPeliculaAsync(int idpelicula)
        {
            //mirar pa hacer un nuevo metodo en la api para hacer el find
            //de pelicula simple
            string request = "api/peliculas/" + idpelicula;
            Pelicula peli = await this.CallApiAsync<Pelicula>(request);
            return peli;
        }

        public async Task<ViewPeliculaCompleta> FindPeliculaCompletaAsync(int idpelicula)
        {
            string request = "api/peliculas/" + idpelicula;
            ViewPeliculaCompleta peliCompleta = await this.CallApiAsync<ViewPeliculaCompleta>(request);
            return peliCompleta;
        }

        public async Task<Serie> FindSerieAsync(int idserie)
        {
            //mirar pa hacer un nuevo metodo en la api para hacer el find
            //de serie simple
            string request = "api/series/" + idserie;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;
        }

        public async Task<ViewSerieCompleta> FindSerieCompletaAsync(int idserie)
        {
            string request = "api/series/" + idserie;
            ViewSerieCompleta serieCompleta = await this.CallApiAsync<ViewSerieCompleta>(request);
            return serieCompleta;
        }

        public async Task<Usuario> PerfilUsuarioAsync()
        {
            string token = this.GetTokenUser();
            string request = "api/usuarios/perfilusuario";
            Usuario user = await this.CallApiAsync<Usuario>(request, token);
            return user;
        }

        public Task<Usuario> FindUsuarioAsync(int idUsuario)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ActoresPelicula>> GetActoresByPeliculaAsync(int idpelicula)
        {
            string request = "api/peliculas/getactoresbypelicula/" + idpelicula;
            List<ActoresPelicula> actores = await this.CallApiAsync<List<ActoresPelicula>>(request);
            return actores;
        }

        public async Task<List<ActoresSerie>> GetActoresBySerieAsync(int idserie)
        {
            string request = "api/series/getactoresbyserie/" + idserie;
            List<ActoresSerie> actores = await this.CallApiAsync<List<ActoresSerie>>(request);
            return actores;
        }

        public async Task<List<ComentarioPelicula>> GetComentariosPeliculaAsync(int idpelicula)
        {
            string request = "api/peliculas/getcomentariospelicula/" + idpelicula;
            List<ComentarioPelicula> comentarios = await this.CallApiAsync<List<ComentarioPelicula>>(request);
            return comentarios;
        }

        public async Task<List<ComentarioSerie>> GetComentariosSerieAsync(int idserie)
        {
            string request = "api/series/getcomentariosserie/" + idserie;
            List<ComentarioSerie> comentarios = await this.CallApiAsync<List<ComentarioSerie>>(request);
            return comentarios;
        }

        public async Task<List<ViewAllPelicula>> GetPeliculasFavoritosAsync()
        {
            string token = this.GetTokenUser();
            string request = "api/favoritos/getpeliculasfavoritos";
            List<ViewAllPelicula> peliculasFavs = await this.CallApiAsync<List<ViewAllPelicula>>(request, token);
            return peliculasFavs;
        }

        public Task<List<ViewAllPelicula>> GetFavoritosPeliculaByUsuarioAsync(int idusuario)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ViewAllSerie>> GetSeriesFavoritosAsync()
        {
            string token = this.GetTokenUser();
            string request = "api/favoritos/getseriesfavoritos";
            List<ViewAllSerie> seriesFavs = await this.CallApiAsync<List<ViewAllSerie>>(request, token);
            return seriesFavs;
        }

        public Task<List<ViewAllSerie>> GetFavoritosSerieByUsuarioAsync(int idusuario)
        {
            throw new NotImplementedException();
        }

        public Task<List<Pelicula>> GetFavoritosPeliculaSessionAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }


        public async Task<List<Genero>> GetGenerosAsync()
        {
            string request = "api/generos";
            List<Genero> generos = await this.CallApiAsync<List<Genero>>(request);
            return generos;
        }

        public async Task<int> GetMaxIdPelicula()
        {
            string request = "api/peliculas/maxidpelicula";
            int idmaxpelicula = await this.CallApiAsync<int>(request);
            return idmaxpelicula;
        }

        public async Task<List<Pelicula>> GetPeliculasAsync()
        {
            string request = "api/peliculas";
            List<Pelicula> pelis = await this.CallApiAsync<List<Pelicula>>(request);
            return pelis;
        }

        public Task<Pelicula> GetPeliculasByGenero(int idgenero)
        {
            throw new NotImplementedException();
        }

        public Task<List<ViewPeliculaCompleta>> GetPeliculasCompletasAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Pelicula>> GetPeliculasPopularesAsync()
        {
            string request = "api/peliculas/getpeliculaspopulares";
            List<Pelicula> pelisPopus = await this.CallApiAsync<List<Pelicula>>(request);
            return pelisPopus;
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "api/series";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public Task<List<ViewSerieCompleta>> GetSeriesCompletasAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Serie>> GetSeriesPopularesAsync()
        {
            string request = "api/series/getseriespopulares";
            List<Serie> seriesPopus = await this.CallApiAsync<List<Serie>>(request);
            return seriesPopus;
        }

        public async Task CreateComentarioPeliculaAsync(int idpelicula, string comentario)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/peliculas/createcomentariopelicula";
                string token = this.GetTokenUser();
                client.BaseAddress = new Uri(this.UrlCineBook);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                ComentarioPeliculaPost comment = new ComentarioPeliculaPost
                {
                    IdPelicula = idpelicula,
                    Comentario = comentario
                };
                string json = JsonConvert.SerializeObject(comment);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("El comentario se creó correctamente.");
                }
                else
                {
                    Console.WriteLine("Hubo un problema al crear el comentario.");
                }
            }
        }


        public Task InsertComentarioPeliculaAsync(int idusuario, int idpelicula, DateTime fechaComentario, string comentario)
        {
            throw new NotImplementedException();
        }

        public async Task CreateComentarioSerieAsync(int idserie, string comentario)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/series/createcomentarioserie";
                string token = this.GetTokenUser();
                client.BaseAddress = new Uri(this.UrlCineBook);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                ComentarioSeriePost comment = new ComentarioSeriePost
                {
                    IdSerie = idserie,
                    Comentario = comentario
                };
                string json = JsonConvert.SerializeObject(comment);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("El comentario se creó correctamente.");
                }
                else
                {
                    Console.WriteLine("Hubo un problema al crear el comentario.");
                }
            }
        }

        public Task InsertComentarioSerieAsync(int idusuario, int idserie, DateTime fechaComentario, string comentario)
        {
            throw new NotImplementedException();
        }

        public async Task InsertFavoritoPeliculaAsync(int idusuario, int idpelicula)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/favoritos/insertpeliculafavoritos/" + idpelicula;
                string token = this.GetTokenUser();
                client.BaseAddress = new Uri(this.UrlCineBook);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.PostAsync(request, null);
            }
        }

        public async Task InsertFavoritoSerieAsync(int idusuario, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/favoritos/insertseriefavoritos/" + idserie;
                string token = this.GetTokenUser();
                client.BaseAddress = new Uri(this.UrlCineBook);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response = await client.PostAsync(request, null);
            }
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

        public async Task<Usuario> RegisterUsuarioAsync(string nombre, string email, string password, string fechaNac)
        {
            throw new NotImplementedException();
        }

        public async Task<RegisterModel> RegisterUsuarioModelAsync(string nombre, string email, string password, string fechaNac)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/usuarios/register";
                client.BaseAddress = new Uri(this.UrlCineBook);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                RegisterModel model = new RegisterModel
                {
                    Nombre = nombre, 
                    Email = email,
                    Password = password,
                    FechaNac = fechaNac
                };
                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("El usuario se creó correctamente.");
                }
                else
                {
                    Console.WriteLine("Hubo un problema al crear el usuario.");
                }
                return model;
            }
        }

        public async Task UpdateUsuarioAsync(int idusuario, string nombre, string email, string fechaNac)
        {
            using(HttpClient client = new HttpClient())
            {
                string request = "api/usuarios/updateusuario";
                string token = this.GetTokenUser();
                client.BaseAddress = new Uri(this.UrlCineBook);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                UpdateUsuarioModel model = new UpdateUsuarioModel
                {
                    Nombre = nombre,
                    Email = email,
                    FechaNac = fechaNac
                };
                string json = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("El usuario se ha actualizado correctamente.");
                }
                else
                {
                    Console.WriteLine("Hubo un problema al actualizar el usuario.");
                }
            }
        }
    }
}
