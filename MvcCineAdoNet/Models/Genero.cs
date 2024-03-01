using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("Genero")]
    public class Genero
    {
        [Key]
        [Column("idGenero")]
        public int IdGenero { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
    }
}
