using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("V_ALL_SERIE")]
    public class ViewAllSerie
    {
        [Key]
        [Column("idFavorito")]
        public int IdFavorito { get; set; }
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
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
