using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("Personaje")]
    public class Personaje
    {
        [Key]
        [Column("idPersonaje")]
        public int IdPersonaje { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("idActor")]
        public int IdActor { get; set; }
        [Column("idPelicula")]
        public int IdPelicula { get; set; }
        [Column("idSerie")]
        public int IdSerie { get; set; }
        [Column("imagen")]
        public string Imagen { get; set; }
    }
}
