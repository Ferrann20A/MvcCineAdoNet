namespace MvcCineAdoNet.Models
{
    public class ResumenDetailsSerie
    {
        public ViewSerieCompleta SerieCompleta { get; set; }
        public List<ActoresSerie> ActoresSerie { get; set; }
        public List<ComentarioSerie> ComentariosSerie { get; set; }
    }
}
