using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using DAL.NLog;
using ORM;

namespace DAL
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly EntityModel _context;
        private readonly ILogForum _logForum;
        public CategoryRepository(IUnitOfWork uow ,ILogForum logForum)
        {
            _logForum = logForum;
            _context = uow.Context;
        }

        public void Create(Category category)
        {
            _context.Set<Category>().Add(category);
            _logForum.Info($"Create '{category.Name}' category. | {DateTime.Now}");
        }

        public void Delete(int categoryId)
        {
            var delCategory = _context.Set<Category>().FirstOrDefault(c => c.Id == categoryId);
            if (ReferenceEquals(delCategory, null))
            {
                var ex= new ArgumentException("The category isn't exist");
                _logForum.Error(ex,$"{ex.Message} | {DateTime.Now}");

                throw ex;
            }

            _context.Set<Category>().Remove(delCategory);
            _logForum.Info($"Delete '{delCategory.Name}' category. | {DateTime.Now}");
        }

        public void Update(Category category)
        {
            var upCategory = _context.Set<Category>().FirstOrDefault(c => c.Id == category.Id);
            if (ReferenceEquals(upCategory, null))
            {
                var ex = new ArgumentException("The category isn't exist");
                _logForum.Error(ex, $"{ex.Message} | {DateTime.Now}");

                throw ex;
            }

            upCategory.Name = category.Name;
            upCategory.Description = category.Description;
            _logForum.Info($"Update '{category.Name}' category. | {DateTime.Now}");
        }

        public IEnumerable<Category> GetAll()
        {
            _logForum.Info($"Get all categories. | {DateTime.Now}");
            return _context.Set<Category>();
        }

        public Category GetById(int id)
        {
          _logForum.Info($"Get category by id={id}. | {DateTime.Now}");
            return _context.Set<Category>().FirstOrDefault(c => c.Id == id);
        }

        public Category GetByPredicate(Expression<Func<Category, bool>> fun)
        {
            _logForum.Info($"Get category predicate {fun}. | {DateTime.Now}");
            return _context.Set<Category>().FirstOrDefault(fun);
        }
        
    }
}
