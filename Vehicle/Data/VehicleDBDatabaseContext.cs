using Microsoft.EntityFrameworkCore;

namespace Vehicle.Data
{
    public class VehicleDBDatabaseContext: DbContext
    {
        public VehicleDBDatabaseContext(DbContextOptions<VehicleDBDatabaseContext> options) : base(options)
        {

        }
        public DbSet<Vehicle.Model.Vehicle> Vehicle { get; set; }
    }
}
