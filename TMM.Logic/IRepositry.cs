using System.ComponentModel.DataAnnotations.Schema;
using TMM.Database;

namespace TMM.Logic
{
    public interface IRepository<T> where T : EntityBase
    {
        T GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IEnumerable<T> GetByPredicate(Func<T, bool> cust);

        IEnumerable<T> GetAll();
    }




}
