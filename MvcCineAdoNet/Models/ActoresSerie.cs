using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("V_ACTORES_SERIE")]
    public class ActoresSerie
    {
        [Column("idSerie")]
        public int IdSerie { get; set; }
        [Key]
        [Column("nombre_actor")]
        public string NombreActor { get; set; }
        [Column("nombre_personaje")]
        public string NombrePersonaje { get; set; }
        [Column("imagen_personaje")]
        public string Imagen { get; set; }
    }
}
