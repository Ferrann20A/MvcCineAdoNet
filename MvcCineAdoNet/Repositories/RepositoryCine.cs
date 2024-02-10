using MvcCineAdoNet.Models;
using System.Data;
using System.Data.SqlClient;

namespace MvcCineAdoNet.Repositories
{
    public class RepositoryCine
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public RepositoryCine()
        {
            string connectionString = @"Data Source=LOCALHOST\SQLEXPRESS;Initial Catalog=CINEBBDD;Persist Security Info=True;User ID=SA;Password=MCSD2023;";
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
        }

        public List<Medio> GetPeliculas()
        {
            string sql = "select * from medio where tipomedioid = 1";
            this.com.CommandType = CommandType.Text;
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();
            List<Medio> peliculas = new List<Medio>();
            while (this.reader.Read())
            {
                Medio peli = new Medio();
                peli.MedioId = int.Parse(this.reader["MedioId"].ToString());
                peli.TipoMedioId = int.Parse(this.reader["TipoMedioId"].ToString());
                peli.Titulo = this.reader["Titulo"].ToString();
                peli.Director = this.reader["Director"].ToString();
                peli.AnioEstreno = this.reader["AnioEstreno"].ToString();
                peli.ClasificacionEdad = this.reader["ClasificacionEdad"].ToString();
                peli.Sinopsis = this.reader["Sinopsis"].ToString();
                peli.DuracionMins = int.Parse(this.reader["DuracionMinutos"].ToString());
                peli.PuntuacionMedia = int.Parse(this.reader["PuntuacionMedia"].ToString());
                peli.Estado = this.reader["Estado"].ToString();
                peli.Imagen = this.reader["Imagen"].ToString();
                peli.GeneroId = int.Parse(this.reader["GeneroId"].ToString());
                peliculas.Add(peli);
            }
            this.reader.Close();
            this.cn.Close();
            return peliculas;
        }
    }
}
