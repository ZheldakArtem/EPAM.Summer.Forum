using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        T GetByPredicate(Expression<Func<T, bool>> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
