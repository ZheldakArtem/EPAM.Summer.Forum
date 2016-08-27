using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace BLL.Interface.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAll();
        Role GetById(int id);
        void Add(Role role);
        void Remove(int roleId);
        void Edit(Role role);
        Role GetByName(string nameRole);
    }
}
