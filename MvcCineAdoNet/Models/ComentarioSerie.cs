using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("V_COMENTARIOS_SERIE")]
    public class ComentarioSerie
    {
        [Key]
        [Column("idComentario")]
        public int IdComentario { get; set; }
        [Column("idSerie")]
        public int IdSerie { get; set; }
        [Column("Usuario")]
        public string Usuario { get; set; }
        [Column("fechaComentario")]
        public DateTime FechaComentario { get; set; }
        [Column("comentario")]
        public string Comentario { get; set; }
    }
}
