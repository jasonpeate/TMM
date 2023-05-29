using Microsoft.EntityFrameworkCore;
using TMM.Database;

namespace TMM.Logic
{
    public class CustomerHelper : ICustomerHelper
    {
        private TMMDbContext db;

        public CustomerHelper(TMMDbContext db)
        {
            this.db = db;
            this.db.InitData();
        }

        public IEnumerable<Customer> GetCustomers(bool ActiveOnly)
        {
            if (ActiveOnly)
            {
                return db.Customers.Where(a => a.Active).Include(a => a.Addresses);
            }

            return db.Customers.Include(a => a.Addresses);
        }

        public Customer GetCustomer(int ID)
        {

            return db.Customers.Single(a => a.Id == ID);
        }

        public (bool Result, string Message) DeleteAddress(int CustomerID, int AddressID)
        {
            Customer c = db.Customers.SingleOrDefault(a => a.Id == CustomerID);

            if (c is null)
            {
                return (false, "Invalid Customer Passed in");
            }
            else if (!c.Addresses.Any(a => a.Id == AddressID)) // address exists
            {
                return (false, "Invalid Adddress id passed in");
            }
            else if (c.Addresses.Count == 1)
            {
                return (false, "Address is the last address for the customer, cannot be deleted");
            }
            else
            {
                Address toDelete = c.Addresses.Single(a => a.Id == CustomerID);
                db.Addresses.Remove(toDelete);

                c.Addresses.First(a => a.Id != AddressID).MainAddress = true;

                return (true, "Address Deleted");
            }
        }

    }


    public interface ICustomerHelper
    {
        IEnumerable<Customer> GetCustomers(bool ActiveOnly);
        Customer GetCustomer(int ID);
    }
}