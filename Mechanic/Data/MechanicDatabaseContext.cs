using Microsoft.EntityFrameworkCore;
namespace Mechanic.Data
{
    public class MechanicDatabaseContext: DbContext
    {
        public MechanicDatabaseContext(DbContextOptions<MechanicDatabaseContext> options) : base(options)
        {

        }
        public DbSet<Mechanic.Model.Mechanic> Mechanic { get; set; }
    }
}
