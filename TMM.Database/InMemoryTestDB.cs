using Microsoft.EntityFrameworkCore;

namespace TMM.Database
{
    public class InMemoryTestDB : DbContext, ITMMDbContext
    {
        public InMemoryTestDB()
  : base(GetDB()) 
        {
            InitData();
        }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasAlternateKey(a => new { a.Forename, a.SureName });

        }

        private static DbContextOptions<InMemoryTestDB> GetDB()
        {
            return new DbContextOptionsBuilder<InMemoryTestDB>().UseInMemoryDatabase("TMMM").Options;
        }

        public void InitData()
        {
            //3 Customers, 1 Being InActive



            Customer[] _cust = new Customer[3]
            {
                AddCustomer1(),
                AddCustomer2(),
                AddCustomer3(),
            };

            this.Customers.AddRange(_cust);


            this.SaveChanges();

            Customer AddCustomer1()
            {
                Customer c1 = new()
                {
                    Active = true,
                    EmailAddress = "Customer1EmatilAddress",
                    Forename = "Customer1ForeName",
                    SureName = "Customer1Surname",
                    MobileNo = "Customer1MobileNumber",
                    Title = "HHHHH",
                    Addresses = new List<Address>()
                };

                Address[] ad = new Address[3]
                {
                    new Address() { AddressLine1 = "AddressLine1", Country = "Random", County = "Random1", MainAddress = true, Postcode = "asfdsfas", Town = "safdasfasdfas", Customer = c1},
                    new Address() { AddressLine1 = "AddressLine2", Country = "Random1", County = "Random5", MainAddress = false, Postcode = "agfsdfsadfas", Town = "afasdfas", Customer = c1},
                    new Address() { AddressLine1 = "AddressLine2", County = "Random5", MainAddress = false, Postcode = "agfsdfsadfas", Town = "afasdfas", Customer = c1},
                };

                foreach (var a in ad)
                {
                    c1.Addresses.Add(a);
                }

                return c1;
            }

            Customer AddCustomer2()
            {
                Customer c1 = new()
                {
                    Active = false,
                    EmailAddress = "Customer2EmatilAddress",
                    Forename = "Customer2ForeName",
                    SureName = "Customer2Surname",
                    MobileNo = "Customer2MobileNumber",
                    Title = "HHHHH",
                    Addresses = new List<Address>()
                };

                Address[] ad = new Address[1]
                {
                    new Address() { AddressLine1 = "AddressLine1", Country = "Random", County = "Random1", MainAddress = true, Postcode = "asfdsfas", Town = "safdasfasdfas", Customer = c1},
             
                };

                foreach (var a in ad)
                {
                    c1.Addresses.Add(a);
                }

                return c1;
            }


            Customer AddCustomer3()
            {
                Customer c1 = new()
                {
                    Active = true,
                    EmailAddress = "Customer3EmatilAddress",
                    Forename = "Customer3ForeName",
                    SureName = "Customer3Surname",
                    MobileNo = "Customer3MobileNumber",
                    Title = "HHHffffHH",
                    Addresses = new List<Address>()
                };

                Address[] ad = new Address[3]
                {
                    new Address() { AddressLine1 = "AddressLine1", Country = "Random", County = "Random1", MainAddress = true, Postcode = "asfdsfas", Town = "safdasfasdfas", Customer = c1},
                    new Address() { AddressLine1 = "AddressLine1", Country = "Random", County = "Random1", MainAddress = false, Postcode = "asfdsfas", Town = "safdasfasdfas", Customer = c1},
                    new Address() { AddressLine1 = "AddressLine1", Country = "Random", County = "Random1", MainAddress = false, Postcode = "asfdsfas", Town = "safdasfasdfas", Customer = c1},

                };

                foreach (var a in ad)
                {
                    c1.Addresses.Add(a);
                }

                return c1;
            }

        }
    }


}
