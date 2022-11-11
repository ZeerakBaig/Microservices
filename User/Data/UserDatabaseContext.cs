using Microsoft.EntityFrameworkCore;
namespace User.Data
{
    public class UserDatabaseContext : DbContext
    {
        public UserDatabaseContext(DbContextOptions<UserDatabaseContext> options) : base(options)
        {

        }
        public DbSet<User.Model.User> User { get; set; }
    }
}
