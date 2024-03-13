using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("Favorito_Serie")]
    public class Favorito_Serie
    {
        [Key]
        [Column("idFavorito")]
        public int IdFavorito { get; set; }
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
        [Column("idSerie")]
        public int IdSerie { get; set; }
    }
}
