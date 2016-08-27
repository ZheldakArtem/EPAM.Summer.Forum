using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using ORM;

namespace DAL
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly EntityModel _context;

        public CategoryRepository(IUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public void Create(Category category)
        {
            _context.Set<Category>().Add(category);
        }

        public void Delete(int categoryId)
        {
            var delCategory = _context.Set<Category>().FirstOrDefault(c => c.Id == categoryId);
            if (ReferenceEquals(delCategory, null))
                throw new ArgumentException("The category isn't exist");

            _context.Set<Category>().Remove(delCategory);
        }

        public void Update(Category category)
        {
            var upCategory = _context.Set<Category>().FirstOrDefault(c => c.Id == category.Id);
            if (ReferenceEquals(upCategory, null))
                throw new ArgumentException("The category isn't exist");

            upCategory.Name = category.Name;
            upCategory.Description = category.Description;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Set<Category>();
        }

        public Category GetById(int id)
        {
            return _context.Set<Category>().FirstOrDefault(c => c.Id == id);
        }

        public Category GetByPredicate(Expression<Func<Category, bool>> fun)
        {
            return _context.Set<Category>().FirstOrDefault(fun);
        }
        
    }
}
