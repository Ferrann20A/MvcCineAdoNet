using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("Pelicula")]
    public class Pelicula
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
        [Column("idGenero")]
        public int IdGenero { get; set; }
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
