using Microsoft.EntityFrameworkCore;

namespace Purchase.Data
{
    public class PurchaseDBDatabaseContext : DbContext
    {
        public PurchaseDBDatabaseContext(DbContextOptions<PurchaseDBDatabaseContext> options) : base(options)
        {

        }
        public DbSet<Purchase.Model.Purchase> Purchase { get; set; }
    }
}
