using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcCineAdoNet.Models
{
    [Table("V_SERIE_COMPLETA")]
    public class ViewSerieCompleta
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
