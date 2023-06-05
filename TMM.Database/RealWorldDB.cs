using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace TMM.Database
{
    public class RealWorldDB : DbContext, ITMMDbContext
    {
        public RealWorldDB()
  : base(GetDB()) 
        {
            Database.EnsureCreated();
            Database.Migrate(); //TODO : Should not be like this, Should be controlled better            
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(b =>
            {
                b.HasIndex(e => new { e.Forename, e.SureName }).IsUnique();
            });

            //modelBuilder.Entity<Address>().Property(b => b.Country).HasDefaultValueSql("UK"); //TODO : Broken come back to

        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder) 
        { 
            configurationBuilder.Conventions.Remove(typeof(CascadeDeleteConvention)); 
        }

        private static DbContextOptions<RealWorldDB> GetDB()
        {
            string ConnectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Integrated Security=SSPI;Initial Catalog=MyAppDB;";

            return new DbContextOptionsBuilder<RealWorldDB>().UseSqlServer(ConnectionString).Options;
        }
    }
}