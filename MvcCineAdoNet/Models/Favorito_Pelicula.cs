using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("Favorito_Pelicula")]
    public class Favorito_Pelicula
    {
        [Key]
        [Column("idFavorito")]
        public int IdFavorito { get; set; }
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
        [Column("idPelicula")]
        public int IdPelicula { get; set; }
    }
}
