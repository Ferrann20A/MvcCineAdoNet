using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using MvcCineAdoNet.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;

#region PROCEDIMIENTOS_ALMACENADOS

//alter procedure SP_INSERT_MEDIO
//(@tipomedioid int, @titulo nvarchar(max),
//@director nvarchar(max), @anioestreno int, @clasificacion nvarchar(max),
//@sinopsis nvarchar(max), @duracionmins int, @puntuacionmedia int,
//@estado nvarchar(max), @imagen nvarchar(max), @generoid int)
//as
//	declare @nextId int
//	select @nextId = max(medioid) + 1 from medio
//	INSERT INTO Medio
//	VALUES (
//		@nextId,
//        @tipomedioid,
//        @titulo,
//        @director,
//        @anioestreno,
//        @clasificacion,
//        @sinopsis,
//        @duracionmins,
//        @puntuacionmedia,
//        @estado,
//        @imagen,
//        @generoid
//	);
//go

#endregion

namespace MvcCineAdoNet.Repositories
{
    public class RepositoryCineSQLServer: IRepositoryCine
    {
        private DataTable tablaMedios;
        private DataTable tablaActores;
        private DataTable tablaTiposMedio;
        private DataTable tablaGeneros;
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
            string sqlTiposMedio = "select * from tipomedio";
            string sqlGeneros = "select * from genero";
            SqlDataAdapter adMedios = new SqlDataAdapter(sqlMedios, connectionString);
            SqlDataAdapter adActores = new SqlDataAdapter(sqlActores, connectionString);
            SqlDataAdapter adTiposMedio = new SqlDataAdapter(sqlTiposMedio, connectionString);
            SqlDataAdapter adGeneros = new SqlDataAdapter(sqlGeneros, connectionString);
            this.tablaMedios = new DataTable();
            this.tablaActores = new DataTable();
            this.tablaTiposMedio = new DataTable();
            this.tablaGeneros = new DataTable();
            adMedios.Fill(tablaMedios);
            adActores.Fill(tablaActores);
            adTiposMedio.Fill(tablaTiposMedio);
            adGeneros.Fill(tablaGeneros);
        }

        public void DeleteActor(int idActor)
        {
            string sql = "delete from actor where actorid = @idactor";
            this.com.Parameters.AddWithValue("@idactor", idActor);
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
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

        public Medio FindMedio(int idMedio)
        {
            var consulta = from datos in this.tablaMedios.AsEnumerable()
                           where datos.Field<int>("MedioID") == idMedio
                           select datos;
            var row = consulta.First();
            Medio medio = new Medio
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
                medio.DuracionMins = row.Field<int>("DuracionMinutos");
            }
            else
            {
                // Asignar un valor por defecto o dejar como 0 si es apropiado
                medio.DuracionMins = 0;
            }
            return medio;
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
            var consulta = from datos in this.tablaGeneros.AsEnumerable()
                           select datos;
            List<Genero> generos = new List<Genero>();
            foreach(var row in consulta)
            {
                Genero gen = new Genero
                {
                    GeneroId = row.Field<int>("GeneroID"),
                    NombreGenero = row.Field<string>("NombreGenero"),
                    Descripcion = row.Field<string>("DescripcionGenero")
                };
                generos.Add(gen);
            }
            return generos;
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
            var consulta = from datos in this.tablaTiposMedio.AsEnumerable()
                           select datos;
            List<TipoMedio> tiposmedio = new List<TipoMedio>();
            foreach(var row in consulta)
            {
                TipoMedio tipoMedio = new TipoMedio
                {
                    TipoMedioId = row.Field<int>("TipoMedioID"),
                    Nombre = row.Field<string>("NombreTipo")
                };
                tiposmedio.Add(tipoMedio);
            }
            return tiposmedio;
        }

        public void InsertMedio(int idTipoMedio, string titulo, string director,
            int anioEstreno, string clasificacionEdad, string sinopsis, int duracionmins,
            int puntuacionmedia, string estado, string imagen, int idGenero)
        {
            this.com.Parameters.AddWithValue("@tipomedioid", idTipoMedio);
            this.com.Parameters.AddWithValue("@titulo", titulo);
            this.com.Parameters.AddWithValue("@director", director);
            this.com.Parameters.AddWithValue("@anioestreno", anioEstreno);
            this.com.Parameters.AddWithValue("@clasificacion", clasificacionEdad);
            this.com.Parameters.AddWithValue("@sinopsis", sinopsis);
            this.com.Parameters.AddWithValue("@duracionmins", duracionmins);
            this.com.Parameters.AddWithValue("@puntuacionmedia", puntuacionmedia);
            this.com.Parameters.AddWithValue("@estado", estado);
            this.com.Parameters.AddWithValue("@imagen", imagen);
            this.com.Parameters.AddWithValue("@generoid", idGenero);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = "SP_INSERT_MEDIO";
            this.cn.Open();
            int af = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();
        }
    }
}
