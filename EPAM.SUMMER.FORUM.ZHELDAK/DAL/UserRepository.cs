using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using DAL.Interface.Repository;
using DAL.NLog;
using ORM;

namespace DAL
{
    public class UserRepository : IRepository<User>
    {
        private readonly ILogForum _logForum;
        private readonly EntityModel _context;

        public UserRepository(IUnitOfWork uow, ILogForum logForum)
        {
            _logForum = logForum;
            _context = uow.Context;
        }
        public void Create(User user)
        {
            _logForum.Info($"Create user => {user.FirstName} {user.LastName}. | {DateTime.Now}");
            _context.Set<User>().Add(user);
        }

        public void Update(User user)
        {
            var upUser = _context.Set<User>().FirstOrDefault(c => c.Id == user.Id);
            if (ReferenceEquals(upUser, null))
            {
                var ex = new ArgumentException("The user isn't exist");
                _logForum.Error(ex, $"{ex.Message} | {DateTime.Now}");

                throw ex;
            }

            upUser.Birthday = user.Birthday;
            upUser.Email = user.Email;
            upUser.LastName = user.LastName;
            upUser.FirstName = user.FirstName;
            upUser.Photo = user.Photo;
            upUser.Password = user.Password;
            _logForum.Info($"Update user => {user.FirstName} {user.LastName}. | {DateTime.Now}");
        }

        public void Delete(int userId)
        {
            var delUser = _context.Set<User>().FirstOrDefault(c => c.Id == userId);
            if (ReferenceEquals(delUser, null))
            {
                var ex = new ArgumentException("The user isn't exist");
                _logForum.Error(ex, $"{ex.Message} | {DateTime.Now}");

                throw ex;
            }

            _context.Set<User>().Remove(delUser);
            _logForum.Info($"Delete user => {delUser.FirstName} {delUser.LastName}. | {DateTime.Now}");
        }

        public IEnumerable<User> GetAll()
        {
            _logForum.Info($"Get all users. | {DateTime.Now}");
            return _context.Set<User>();
        }
        public User GetById(int id)
        {
            _logForum.Info($"Get user by id={id}. | {DateTime.Now}");
            return _context.Set<User>().FirstOrDefault(c => c.Id == id);
        }
        public User GetByPredicate(Expression<Func<User, bool>> fun)
        {
            _logForum.Info($"Get role by predicate =>{fun}. | {DateTime.Now}");
            return _context.Set<User>().FirstOrDefault(fun);
        }
    }
}
