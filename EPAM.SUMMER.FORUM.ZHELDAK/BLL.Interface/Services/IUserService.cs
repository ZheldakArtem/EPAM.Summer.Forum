using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        void CreateUser(User user);
        void DeleteUser(int userId);
        void UpdateUser(User user);
        User GetUser(int id);
        IEnumerable<User> GetAllUsers();
        User GetByEmail(string email);
    }
}
