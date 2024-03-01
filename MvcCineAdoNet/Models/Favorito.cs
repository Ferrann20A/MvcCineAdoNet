using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("Favorito")]
    public class Favorito
    {
        [Key]
        [Column("idFavorito")]
        public int IdFavorito { get; set; }
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
        [Column("idPelicula")]
        public int IdPelicula { get; set; }
        [Column("idSerie")]
        public int IdSerie { get; set; }
        [Column("fechaAgregado")]
        public DateTime FechaAgregado { get; set; }
    }
}
