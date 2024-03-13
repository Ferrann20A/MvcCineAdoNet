using Microsoft.EntityFrameworkCore;
using MvcCineAdoNet.Models;

namespace MvcCineAdoNet.Data
{
    public class CineBookContext: DbContext
    {
        public CineBookContext(DbContextOptions<CineBookContext> options)
            :base(options) { }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Serie> Series { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<ViewAllPelicula> ViewAllPeliculas { get; set; }
        public DbSet<ViewAllSerie> ViewAllSeries { get; set; }

        public DbSet<ViewPeliculaCompleta> PeliculasCompletas { get; set; }
        public DbSet<ViewSerieCompleta> SeriesCompletas { get; set; }
    }
}
