using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using ORM;
using DAL;
using System.Linq.Expressions;

namespace DAL
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly EntityModel _context;

        public RoleRepository(IUnitOfWork uow)
        {
            _context = uow.Context;
        }
        public void Create(Role role)
        {
            _context.Set<Role>().Add(role);
        }

        public void Update(Role role)
        {
            var upRole = _context.Set<Role>().FirstOrDefault(c => c.Id == role.Id);
            if (ReferenceEquals(upRole, null))
                throw new ArgumentException("The role isn't exist");

            upRole.Description = role.Description;
            upRole.Name = role.Name;
        }

        public void Delete(int roleId)
        {
            var delRole = _context.Set<Role>().FirstOrDefault(c => c.Id == roleId);
            if (ReferenceEquals(delRole, null))
                throw new ArgumentException("The role is't exist");

            _context.Set<Role>().Remove(delRole);
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Set<Role>();
        }

        public Role GetById(int id)
        {
            return _context.Set<Role>().FirstOrDefault(c => c.Id == id);
        }

        public Role GetByPredicate(Expression<Func<Role, bool>> predicate)
        {
            return _context.Set<Role>().FirstOrDefault(predicate); ;
        }
    }
}
