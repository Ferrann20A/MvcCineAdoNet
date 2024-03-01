using Microsoft.EntityFrameworkCore;

namespace MvcCineAdoNet.Data
{
    public class GestionCineContext: DbContext
    {
        public GestionCineContext(DbContextOptions<GestionCineContext> options)
            :base(options) { }
    }
}
