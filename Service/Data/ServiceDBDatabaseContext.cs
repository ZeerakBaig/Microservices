using Microsoft.EntityFrameworkCore;

namespace Service.Data
{
    public class ServiceDBDatabaseContext : DbContext
    {
        public ServiceDBDatabaseContext(DbContextOptions<ServiceDBDatabaseContext> options) : base(options)
        {

        }
        public DbSet<Service.Model.Service> Service { get; set; }
    }
}
