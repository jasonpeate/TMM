using TMM.Database;

namespace TMM.Logic
{
    public class CustomerHelper : ICustomerHelper
    {
        public IEnumerable<Customer> GetCustomers(TMMDbContext db, bool ActiveOnly)
        {
            if  (ActiveOnly)
            {
                return db.Customers.Where(a => a.Active);
            }

            return db.Customers;
        }

        public Customer GetCustomer(TMMDbContext db, int ID)
        {      

            return db.Customers.Single(a => a.Id == ID);
        }
    }

    public interface ICustomerHelper
    {
        IEnumerable<Customer> GetCustomers(TMMDbContext db, bool ActiveOnly);
        Customer GetCustomer(TMMDbContext db, int ID);
    }
}