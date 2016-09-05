using BLL.Interface;
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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<User> _userRepository;
        public UserService(IUnitOfWork uow, IRepository<User> userRepository)
        {
            _uow = uow;
            _userRepository = userRepository;
        }
        public void CreateUser(User user)
        {
            _userRepository.Create(user);
            _uow.Commit();
        }
        public void DeleteUser(int userId)
        {
            _userRepository.Delete(userId);
            _uow.Commit();
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
            _uow.Commit();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.GetAll();
        }

        public User GetByEmail(string email)
        {
          return  _userRepository.GetByPredicate(c => c.Email == email);
        }

        public User GetUser(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetUserById(int userId)
        {
           return _userRepository.GetById(userId);
        }
    }
}
