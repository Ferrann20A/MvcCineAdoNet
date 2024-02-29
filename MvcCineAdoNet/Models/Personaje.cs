namespace MvcCineAdoNet.Models
{
    public class Personaje
    {
        public int IdPersonaje { get; set; }
        public string Nombre { get; set; }
        public int IdActor { get; set; }
        public int IdPelicula { get; set; }
        public int IdSerie { get; set; }
        public string Imagen { get; set; }
    }
}
