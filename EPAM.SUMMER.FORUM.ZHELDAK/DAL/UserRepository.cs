using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using DAL.Interface.Repository;
using ORM;

namespace DAL
{
    public class UserRepository : IRepository<User>
    {
        private readonly EntityModel _context;

        public UserRepository(IUnitOfWork uow)
        {
            _context = uow.Context;
        }
        public void Create(User user)
        {
            _context.Set<User>().Add(user);
        }

        public void Update(User user)
        {
            var upUser = _context.Set<User>().FirstOrDefault(c => c.Id == user.Id);
            if (ReferenceEquals(upUser, null))
                throw new ArgumentException("The user isn't exist");

            upUser.Birthday = user.Birthday;
            upUser.Email = user.Email;
            upUser.LastName = user.LastName;
            upUser.FirstName = user.FirstName;
            upUser.Photo = user.Photo;
            upUser.Password = user.Password;
        }

        public void Delete(int userId)
        {
            var delUser = _context.Set<User>().FirstOrDefault(c => c.Id == userId);
            if (ReferenceEquals(delUser, null))
                throw new ArgumentException("The user isn't exist");

            _context.Set<User>().Remove(delUser);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Set<User>();
        }
        public User GetById(int id)
        {
            var user = _context.Set<User>().FirstOrDefault(c => c.Id == id);
            if (ReferenceEquals(user, null))
                throw new ArgumentException("The user isn't exist");

            return user;
        }
        public User GetByPredicate(Expression<Func<User, bool>> f)
        {
            return _context.Set<User>().FirstOrDefault(f);
        }
    }
}
