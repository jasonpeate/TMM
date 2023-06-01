using Microsoft.EntityFrameworkCore;

namespace TMM.Database
{
    public interface ITMMDbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }
        int SaveChanges();
    }
}
