using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Interface.Repository;
using ORM;
using System.Linq.Expressions;
using DAL.NLog;

namespace DAL
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly ILogForum _logForum;
        private readonly EntityModel _context;

        public RoleRepository(IUnitOfWork uow,ILogForum logForum)
        {  
                _logForum = logForum;
            _context = uow.Context;
        }
        public void Create(Role role)
        {
            _logForum.Info($"Create role => {role.Name}. | {DateTime.Now}");
            _context.Set<Role>().Add(role);
        }

        public void Update(Role role)
        {
            var upRole = _context.Set<Role>().FirstOrDefault(c => c.Id == role.Id);
            if (ReferenceEquals(upRole, null))
            {
                var ex = new ArgumentException("The role isn't exist");
                _logForum.Error(ex, $"{ex.Message} | {DateTime.Now}");

                throw ex;
            }

            upRole.Description = role.Description;
            upRole.Name = role.Name;
            _logForum.Info($"Update role => {role.Name}. | {DateTime.Now}");
        }

        public void Delete(int roleId)
        {
            var delRole = _context.Set<Role>().FirstOrDefault(c => c.Id == roleId);
            if (ReferenceEquals(delRole, null))
            {
                var ex = new ArgumentException("The role isn't exist");
                _logForum.Error(ex, $"{ex.Message} | {DateTime.Now}");

                throw ex;
            }

            _context.Set<Role>().Remove(delRole);
            _logForum.Info($"Delete role => {delRole.Name}. | {DateTime.Now}");
        }

        public IEnumerable<Role> GetAll()
        {
            _logForum.Info($"Get all roles. | {DateTime.Now}");
            return _context.Set<Role>();
        }

        public Role GetById(int id)
        {
            _logForum.Info($"Get role by id={id}. | {DateTime.Now}");
            return _context.Set<Role>().FirstOrDefault(c => c.Id == id);
        }

        public Role GetByPredicate(Expression<Func<Role, bool>> predicate)
        {
           return _context.Set<Role>().FirstOrDefault(predicate); ;
        }
    }
}
