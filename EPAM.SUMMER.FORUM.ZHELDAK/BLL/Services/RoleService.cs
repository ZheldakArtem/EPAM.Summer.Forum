using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using ORM;

namespace BLL
{
   public class RoleService: IRoleService
    {
       private readonly IRepository<Role> _roleRepository;
       private readonly IUnitOfWork _uow;

       public RoleService(IUnitOfWork uow, IRepository<Role> roleRepository)
       {
           _roleRepository = roleRepository;
           _uow = uow;
       }
       public IEnumerable<Role> GetAll()
       {
          return _roleRepository.GetAll();
       }

       public Role GetById(int id)
       {
          return _roleRepository.GetById(id);
       }

       public void Add(Role role)
       {
          _roleRepository.Create(role);
            _uow.Commit();
       }

       public void Remove(int roleId)
       {
           _roleRepository.Delete(roleId);
            _uow.Commit();
       }

       public void Edit(Role role)
       {
           _roleRepository.Update(role);
            _uow.Commit();
        }

        public Role GetByName(string nameRole)
        {
          return  _roleRepository.GetByPredicate(r=>r.Name==nameRole.ToLower());
        }
    }
}
