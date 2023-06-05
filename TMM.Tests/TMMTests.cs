using TMM.Database;
using TMM.Logic;

namespace TMM.Tests
{
    [TestClass]
    public class TMMTests
    {
        [TestMethod]
        public void GetAllCustomersReturnsCorrectData()
        {
            //arrange
            ICustomerService ic = GetCustomerService(out ITMMDbContext db);

            //act 
            IEnumerable<Customer> allCustomers = ic.GetCustomers(false);

            //assert
            Assert.AreEqual(db.Customers.Count(),allCustomers.Count());
        }

        [TestMethod]
        public void GetActiveCustomersReturnsCorrectData()
        {
            //arrange
            ICustomerService ic = GetCustomerService(out ITMMDbContext db);

            //act 
            IEnumerable<Customer> allCustomers = ic.GetCustomers(true);

            //assert
            Assert.AreEqual(db.Customers.Count(a => a.Active), allCustomers.Count());
        }

        [TestMethod]
        public void DeleteAddressReturnsErrorIfBadCustomerIDPassedIn()
        {
            //arrange
            ICustomerService ic = GetCustomerService(out ITMMDbContext db);

            //act 
            ReponseResult _result = ic.DeleteAddress(99,44);

            //assert
            Assert.IsFalse(_result.Result);
            Assert.AreEqual("Invalid CustomerID Passed in", _result.Message);
        }

        [TestMethod]
        public void DeleteAddressReturnsErrorIfGoodCustomerIDAndBadAddressIDPassedIn()
        {
            //arrange
            ICustomerService ic = GetCustomerService(out ITMMDbContext db);

            //act 
            ReponseResult _result = ic.DeleteAddress(1, 44);

            //assert
            Assert.IsFalse(_result.Result);
            Assert.AreEqual("Invalid Adddress id passed in", _result.Message);
        }

        [TestMethod]
        public void DeleteAddress_Works()
        {
            //arrange
            ICustomerService ic = GetCustomerService(out ITMMDbContext db);

            //act 
            Customer toDeleteCustomer_Address = db.Customers.First();
            Address toDelete_Address = toDeleteCustomer_Address.Addresses.First();

            ReponseResult _result = ic.DeleteAddress(toDeleteCustomer_Address.Id, toDelete_Address.Id);

            //assert

            // Result is correct
            Assert.IsTrue(_result.Result);
            Assert.AreEqual("Address Deleted", _result.Message);
            

            // Address is delete
            Assert.IsFalse(db.Addresses.Any(a => a.Id == toDeleteCustomer_Address.Id));

            // Main Address is switched
            Assert.AreEqual(1,db.Addresses.Count(a => a.CustomerId == toDeleteCustomer_Address.Id && a.MainAddress));
        }

        private ICustomerService GetCustomerService(out ITMMDbContext db)
        {
            db = new InMemoryTestDB();

            return new CustomerService(new CustomerRepository(db), new AddressRepository(db));

      
        }
    }
}