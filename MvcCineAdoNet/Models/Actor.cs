namespace MvcCineAdoNet.Models
{
    public class Actor
    {
        public int ActorId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Pais { get; set; }
        public string Biografia { get; set; }
        public string Imagen { get; set; }
    }
}
