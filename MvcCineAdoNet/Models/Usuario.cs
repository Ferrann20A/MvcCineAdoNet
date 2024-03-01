using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcCineAdoNet.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("idUsuario")]
        public int IdUsuario { get; set; }
        [Column("nombre")]
        public string Nombre { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("contrasenia")]
        public string Contrasenia { get; set; }
        [Column("rol")]
        public string Rol { get; set; }
    }
}
