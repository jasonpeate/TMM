using Microsoft.EntityFrameworkCore;
using TMM.Database;

namespace TMM.Logic
{
    public class CustomerRepository : IRepository<Customer>
    {
        public readonly ITMMDbContext db;

        public CustomerRepository(ITMMDbContext db)
        {
            this.db = db;
        }

        public Customer GetById(int id)
        {
            return db.Customers.Find(id);
        }

        public IEnumerable<Customer> GetByPredicate(Func<Customer, bool> cust)
        {
            return db.Customers.Where(cust);
        }

        public IEnumerable<Customer> GetByPredicate(Func<Customer,bool> cust, bool IncludeAddresses = false)
        {
            if (!IncludeAddresses)
                return GetByPredicate(cust);
            else
                return db.Customers.Include(a => a.Addresses).Where(cust);
        }

        public IEnumerable<Customer> GetAll()
        {
            return db.Customers.Include(a => a.Addresses);
        }

        public void Create(Customer entity)
        {
            db.Customers.Add(entity);
        }
        public void Update(Customer entity)
        {
            db.Customers.Update(entity);
            db.SaveChanges();
        }
        public void Delete(Customer entity)
        {
            db.Customers.Remove(entity);
            db.SaveChanges();
        }
    }
}
