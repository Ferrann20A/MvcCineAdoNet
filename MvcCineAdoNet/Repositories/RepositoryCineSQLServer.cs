using Microsoft.AspNetCore.Components.Web;
using MvcCineAdoNet.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcCineAdoNet.Repositories
{
    public class RepositoryCineSQLServer: IRepositoryCine
    {
        private DataTable tablaMedios;
        private DataTable tablaActores;
        private SqlConnection cn;
        private SqlCommand com;

        public RepositoryCineSQLServer()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=CINEBBDD;Persist Security Info=True;User ID=SA;Password=MCSD2023;";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            string sqlMedios = "select * from medio";
            string sqlActores = "select * from actor";
            SqlDataAdapter adMedios = new SqlDataAdapter(sqlMedios, connectionString);
            SqlDataAdapter adActores = new SqlDataAdapter(sqlActores, connectionString);
            this.tablaMedios = new DataTable();
            this.tablaActores = new DataTable();
            adMedios.Fill(tablaMedios);
            adActores.Fill(tablaActores);
        }

        public void DeleteActor(int idActor)
        {
            throw new NotImplementedException();
        }

        public void DeleteGenero(int idGenero)
        {
            throw new NotImplementedException();
        }

        public void DeleteMedio(int idMedio)
        {
            throw new NotImplementedException();
        }

        public void DeletePersonaje(int idPersonaje)
        {
            throw new NotImplementedException();
        }

        public Medio FindMedio()
        {
            throw new NotImplementedException();
        }

        public List<Actor> GetActores()
        {
            var consulta = from datos in this.tablaActores.AsEnumerable()
                           select datos;
            List<Actor> actores = new List<Actor>();
            foreach(var row in consulta)
            {
                Actor actor = new Actor
                {
                    ActorId = row.Field<int>("ActorId"),
                    Nombre = row.Field<string>("Nombre"),
                    FechaNacimiento = row.Field<DateTime>("FechaNacimiento"),
                    Pais = row.Field<string>("PaisOrigen"),
                    Biografia = row.Field<string>("Biografia"),
                    Imagen = row.Field<string>("Imagen")
                };
                actores.Add(actor);
            }
            return actores;
        }

        public List<Genero> GetGeneros()
        {
            throw new NotImplementedException();
        }

        public List<Medio> GetPeliculas()
        {
            var consulta = from datos in this.tablaMedios.AsEnumerable()
                           where datos.Field<int>("TipoMedioID") == 1
                           select datos;
            List<Medio> peliculas = new List<Medio>();
            foreach (var row in consulta)
            {
                Medio pelicula = new Medio 
                {
                    MedioId = row.Field<int>("MedioID"),
                    TipoMedioId = row.Field<int>("TipoMedioID"),
                    Titulo = row.Field<string>("Titulo"),
                    Director = row.Field<string>("Director"),
                    AnioEstreno = row.Field<int>("AnioEstreno"),
                    ClasificacionEdad = row.Field<string>("ClasificacionEdad"),
                    Sinopsis = row.Field<string>("Sinopsis"),
                    DuracionMins = row.Field<int>("DuracionMinutos"),
                    PuntuacionMedia = row.Field<int>("PuntuacionMedia"),
                    Estado = row.Field<string>("Estado"),
                    Imagen = row.Field<string>("Imagen"),
                    GeneroId = row.Field<int>("GeneroID")
                };
                peliculas.Add(pelicula);
            }
            return peliculas;
        }

        public List<Personaje> GetPersonajes()
        {
            throw new NotImplementedException();
        }

        public List<Medio> GetSeries()
        {
            var consulta = from datos in this.tablaMedios.AsEnumerable()
                           where datos.Field<int>("TipoMedioID") == 2
                           select datos;
            List<Medio> series = new List<Medio>();
            foreach (var row in consulta)
            {
                Medio serie = new Medio
                {
                    MedioId = row.Field<int>("MedioID"),
                    TipoMedioId = row.Field<int>("TipoMedioID"),
                    Titulo = row.Field<string>("Titulo"),
                    Director = row.Field<string>("Director"),
                    AnioEstreno = row.Field<int>("AnioEstreno"),
                    ClasificacionEdad = row.Field<string>("ClasificacionEdad"),
                    Sinopsis = row.Field<string>("Sinopsis"),
                    PuntuacionMedia = row.Field<int>("PuntuacionMedia"),
                    Estado = row.Field<string>("Estado"),
                    Imagen = row.Field<string>("Imagen"),
                    GeneroId = row.Field<int>("GeneroID")
                };

                // Manejar DuracionMins si viene como null
                if (!row.IsNull("DuracionMinutos"))
                {
                    serie.DuracionMins = row.Field<int>("DuracionMinutos");
                }
                else
                {
                    // Asignar un valor por defecto o dejar como 0 si es apropiado
                    serie.DuracionMins = 0;
                }

                series.Add(serie);
            }
            return series;
        }


        public List<TipoMedio> GetTiposMedio()
        {
            throw new NotImplementedException();
        }
    }
}
