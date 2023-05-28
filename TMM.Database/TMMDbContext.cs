using Microsoft.EntityFrameworkCore;

namespace TMM.Database
{
    public class TMMDbContext : DbContext
    {
        public TMMDbContext(DbContextOptions<TMMDbContext> options)
  : base(options) 
        {
            for (int i = 0; i < 100; i++)
            {
                Customers.Add(new Customer()
                {
                    Active = true,
                    //Addresses = new[] {
                    //    new Address()
                    //    {
                    //         AddressLine1 = "sdfasdfsadfas",
                    //         AddressLine2 = "sdfsdfasfasdfas",
                    //         Country = "ffff",
                    //         County = "asdfasfsa",
                    //         MainAddress = true,
                    //         Postcode = "uk",
                    //         Town = "sfsdfsd"
                    //    }
                    //  },
                     EmailAddress = "dfsdafasdfsa",
                     MobileNo = "sadfsadfas",
                     Forename = $"dfasfsdfas{i}",
                     SureName = $"fffff{i}",
                     Title = "dfasdfasdfsa"
                });
            }

            this.SaveChanges();
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasAlternateKey(a => new { a.Forename, a.SureName });
        }
    }


}
