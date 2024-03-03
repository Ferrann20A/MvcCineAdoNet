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
    }
}
