using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("Comentario")]
    public class Comentario
    {
        [Key]
        [Column("idComentario")]
        public int IdComentario { get; set; }
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
        [Column("idPelicula")]
        public int IdPelicula { get; set; }
        [Column("idSerie")]
        public int IdSerie { get; set; }
        [Column("comentario")]
        public string Comentarios { get; set; }
        [Column("fechaComentario")]
        public DateTime FechaComentario { get; set; }
    }
}
