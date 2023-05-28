using Microsoft.EntityFrameworkCore;

namespace TMM.Database
{
    public class TMMDbContext : DbContext
    {
        public TMMDbContext(DbContextOptions<TMMDbContext> options)
  : base(options) { }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}
