using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("Serie")]
    public class Serie
    {
        [Key]
        [Column("idSerie")]
        public int IdSerie { get; set; }
        [Column("titulo")]
        public string Titulo { get; set; }
        [Column("creador")]
        public string Creador { get; set; }
        [Column("anioEstreno")]
        public int AnioEstreno { get; set; }
        [Column("numTemporadas")]
        public int NumTemporadas { get; set; }
        [Column("valoracion")]
        public float Valoracion { get; set; }
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
    }
}
