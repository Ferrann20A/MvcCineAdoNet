namespace MvcCineAdoNet.Models
{
    public class ResumenDetailsPelicula
    {
        public ViewPeliculaCompleta PeliculaCompleta { get; set; }
        public List<ActoresPelicula> ActoresPelicula { get; set; }
        public List<ComentarioPelicula> ComentariosPelicula { get; set; }
    }
}
