namespace MvcCineAdoNet.Models
{
    public class Valoracion
    {
        public int ValoracionId { get; set; }
        public int MedioId { get; set; }
        public int UsuarioId { get; set; }
        public int Puntuacion { get; set; }
        public string Comentario { get; set; }
        public string FechaValoracion { get; set; }
    }
}
