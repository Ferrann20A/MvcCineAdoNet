namespace MvcCineAdoNet.Models
{
    public class ResumenHome
    {
        public ViewPeliculaCompleta PeliculaRandom { get; set; }
        public List<Pelicula> PeliculasPopulares { get; set; }
        public List<Serie> SeriesPopulares { get; set; }
    }
}
