using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("Actor")]
    public class Actor
    {
        [Key]
        [Column("idActor")]
        public int IdActor { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("fechaNacimiento")]
        public DateTime FechaNacimiento { get; set; }
        [Column("biografia")]
        public string Biografia { get; set; }
        [Column("imagen")]
        public string Imagen { get; set; }
    }
}
