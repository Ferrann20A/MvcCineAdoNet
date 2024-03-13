using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcCineAdoNet.Models
{
    [Table("V_PELICULA_COMPLETA")]
    public class ViewPeliculaCompleta
    {
        [Key]
        [Column("idPelicula")]
        public int IdPelicula { get; set; }
        [Column("titulo")]
        public string Titulo { get; set; }
        [Column("director")]
        public string Director { get; set; }
        [Column("anioEstreno")]
        public int AnioEstreno { get; set; }
        [Column("duracion")]
        public int Duracion { get; set; }
        [Column("popularidad")]
        public int Popularidad { get; set; }
        [Column("genero")]
        public string Genero { get; set; }
        [Column("sinopsis")]
        public string Sinopsis { get; set; }
        [Column("trailer")]
        public string Trailer { get; set; }
        [Column("imagen")]
        public string Imagen { get; set; }
        [Column("IMDB")]
        public double IMDB { get; set; }
    }
}
