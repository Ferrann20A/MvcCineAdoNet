namespace MvcCineAdoNet.Models
{
    public class Personaje
    {
        public int PersonajeId { get; set; }
        public string Nombre { get; set; }
        public int MedioId { get; set; }
        public int ActorId { get; set; }
        public string Imagen { get; set; }
    }
}
