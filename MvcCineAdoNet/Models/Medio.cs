namespace MvcCineAdoNet.Models
{
    public class Medio
    {
        public int MedioId { get; set; }
        public int TipoMedioId { get; set; }
        public string Titulo { get; set; }
        public string Director { get; set; }
        public int AnioEstreno { get; set; }
        public string ClasificacionEdad { get; set; }
        public string Sinopsis { get; set; }
        public int DuracionMins { get; set; }
        public int PuntuacionMedia { get; set; }
        public string Estado { get; set; }
        public string Imagen { get; set; }
        public int GeneroId { get; set; }
    }
}
