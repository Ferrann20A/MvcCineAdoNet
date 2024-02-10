namespace MvcCineAdoNet.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FechaRegistro { get; set; }
        public string Rol { get; set; }
    }
}
