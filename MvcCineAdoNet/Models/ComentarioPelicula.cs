using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("V_COMENTARIOS_PELICULA")]
    public class ComentarioPelicula
    {
        [Key]
        [Column("idComentario")]
        public int IdComentario { get; set; }
        [Column("idPelicula")]
        public int IdPelicula { get; set; }
        [Column("Usuario")]
        public string Usuario { get; set; }
        [Column("fechaComentario")]
        public DateTime FechaComentario { get; set; }
        [Column("comentario")]
        public string Comentario { get; set; }
    }
}
