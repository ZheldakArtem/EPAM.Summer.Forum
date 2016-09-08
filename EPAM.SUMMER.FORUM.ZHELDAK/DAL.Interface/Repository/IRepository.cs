using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Repository
{
    /// <summary>
    ///  Defines CRUD methods.
    /// </summary>
    /// <typeparam name="T">The type of elements.</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get all the objects of type T
        /// </summary>
        /// <returns>Collection IEnumerable<T></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get element by id.
        /// </summary>
        /// <param name="id">The unique identifier of element.</param>
        /// <returns>The object of type T</returns>
        T GetById(int id);

        /// <summary>
        /// Get element by predicate.
        /// </summary>
        /// <param name="predicate">The predicate of expression</param>
        /// <returns>The object of type T</returns>
        T GetByPredicate(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create an object of type T.
        /// </summary>
        /// <param name="item">The object that will be created.</param>
        void Create(T item);

        /// <summary>
        /// Update an object of type T.
        /// </summary>
        /// <param name="item">The object that will be updated</param>
        void Update(T item);

        /// <summary>
        /// Delete an object by id.
        /// </summary>
        /// <param name="id">The unique identifier of element.</param>
        void Delete(int id);
    }
}
