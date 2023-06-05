using TMM.Database;

namespace TMM.Logic
{
    public class AddressRepository : IRepository<Address>
    {
        public readonly ITMMDbContext db;

        public AddressRepository(ITMMDbContext db)
        {
            this.db = db;
        }

        public Address GetById(int id)
        {
            return db.Addresses.Find(id);
        }

        public IEnumerable<Address> GetByPredicate(Func<Address, bool> cust)
        {
            return db.Addresses.Where(cust);
        }    

        public IEnumerable<Address> GetAll()
        {
            return db.Addresses;
        }

        public void Create(Address entity)
        {
            db.Addresses.Add(entity);
        }
        public void Update(Address entity)
        {
            db.Addresses.Update(entity);
            db.SaveChanges();
        }
        public void Delete(Address entity)
        {
            db.Addresses.Remove(entity);
            db.SaveChanges();
        }
    }
}
