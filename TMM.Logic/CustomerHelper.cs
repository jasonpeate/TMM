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
            //TODO : Valid customer check here
            return db.Customers.Single(a => a.Id == ID);
        }

        public (bool Result, string Message) DeleteAddress(int CustomerID, int AddressID)
        {
            if (!GetSingleCustomer(CustomerID, out string Message, out Customer c))
            {
                return (false, Message);
            }
            else if (!c.Addresses.Any(a => a.Id == AddressID)) // address does not exists
            {
                return (false, "Invalid Adddress id passed in");
            }
            else if (c.Addresses.Count == 1)
            {
                return (false, "Address is the last address for the customer, cannot be deleted");
            }
            else
            {
                Address toDelete = c.Addresses.Single(a => a.Id == AddressID);
                db.Addresses.Remove(toDelete);

                c.Addresses.First(a => a.Id != AddressID).MainAddress = true;

                db.SaveChanges();

                return (true, "Address Deleted");
            }
        }

        public (bool Result, string Message) SetMainAddress(int CustomerID, int AddressID)
        {
            if (!GetSingleCustomer(CustomerID, out string Message, out Customer c))
            {
                return (false, Message);
            }
            else if (!c.Addresses.Any(a => a.Id == AddressID)) // address does not exists
            {
                return (false, "Invalid Adddress id passed in");
            }
            else
            {
                foreach (Address address in c.Addresses)
                {
                    if (address.Id == AddressID)
                        address.MainAddress = true;
                    else
                        address.MainAddress = false; //TODO : this should be locked down on db level
                }    
                
                db.SaveChanges();

                return (true, "Main Address Updated");
            }
        }

        public (bool Result, string Message) MarkCustomerAsInactive(int CustomerID)
        {
            if (!GetSingleCustomer(CustomerID, out string Message, out Customer c))
            {
                return (false, Message);
            }
            else
            {
                c.Active = false;

                db.SaveChanges();

                return (true, "Customer marked as inactive");
            }
        }

        public (bool Result, string Message) DeleteCustomer(int CustomerID)
        {
            if (!GetSingleCustomer(CustomerID,out string Message, out Customer c))
            {
                return (false, Message);
            }
            else
            {
                db.Customers.Remove(c);

                db.SaveChanges();

                return (true, "Customer deleted");
            }
        }

        private bool GetSingleCustomer(int CustomerID,out string Message, out Customer customer)
        {
            customer = db.Customers.SingleOrDefault(a => a.Id == CustomerID);
            Message = "Invalid CustomerID Passed in";

            return customer != null;

        }

    }


    public interface ICustomerHelper
    {
        IEnumerable<Customer> GetCustomers(bool ActiveOnly);
        Customer GetCustomer(int ID);
        (bool Result, string Message) DeleteAddress(int CustomerID, int AddressID);
        (bool Result, string Message) SetMainAddress(int CustomerID, int AddressID);
        (bool Result, string Message) MarkCustomerAsInactive(int CustomerID);
        (bool Result, string Message) DeleteCustomer(int CustomerID);
    }
}