using Microsoft.EntityFrameworkCore;
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
            InMemoryTestDB db = GetDB();

            ICustomerHelper ic = new CustomerHelper(db);

            //act 
            IEnumerable<Customer> allCustomers = ic.GetCustomers(false);

            //assert
            Assert.AreEqual(db.Customers.Count(),allCustomers.Count());
        }

        [TestMethod]
        public void GetActiveCustomersReturnsCorrectData()
        {
            //arrange
            InMemoryTestDB db = GetDB();

            ICustomerHelper ic = new CustomerHelper(db);

            //act 
            IEnumerable<Customer> allCustomers = ic.GetCustomers(true);

            //assert
            Assert.AreEqual(db.Customers.Count(a => a.Active), allCustomers.Count());
        }

        [TestMethod]
        public void DeleteAddressReturnsErrorIfBadCustomerIDPassedIn()
        {
            //arrange
            InMemoryTestDB db = GetDB();

            ICustomerHelper ic = new CustomerHelper(db);

            //act 
            (bool Result, string Message) _result = ic.DeleteAddress(99,44);

            //assert
            Assert.IsFalse(_result.Result);
            Assert.AreEqual("Invalid CustomerID Passed in", _result.Message);
        }

        [TestMethod]
        public void DeleteAddressReturnsErrorIfGoodCustomerIDAndBadAddressIDPassedIn()
        {
            //arrange
            InMemoryTestDB db = GetDB();

            ICustomerHelper ic = new CustomerHelper(db);

            //act 
            (bool Result, string Message) _result = ic.DeleteAddress(1, 44);

            //assert
            Assert.IsFalse(_result.Result);
            Assert.AreEqual("Invalid Adddress id passed in", _result.Message);
        }

        [TestMethod]
        public void DeleteAddress_Works()
        {
            //arrange
            InMemoryTestDB db = GetDB();

            ICustomerHelper ic = new CustomerHelper(db);

            //act 
            Customer toDeleteCustomer_Address = db.Customers.First();
            Address toDelete_Address = toDeleteCustomer_Address.Addresses.First();

            (bool Result, string Message) _result = ic.DeleteAddress(toDeleteCustomer_Address.Id, toDelete_Address.Id);

            //assert

            // Result is correct
            Assert.IsTrue(_result.Result);
            Assert.AreEqual("Address Deleted", _result.Message);
            

            // Address is delete
            Assert.IsFalse(db.Addresses.Any(a => a.Id == toDeleteCustomer_Address.Id));

            // Main Address is switched
            Assert.AreEqual(1,db.Addresses.Count(a => a.CustomerId == toDeleteCustomer_Address.Id && a.MainAddress));
        }

        private InMemoryTestDB GetDB()
        {
            DbContextOptionsBuilder<InMemoryTestDB> x = new DbContextOptionsBuilder<InMemoryTestDB>();

            x.UseInMemoryDatabase("TMMM");

            return new InMemoryTestDB(x.Options);
        }
    }
}