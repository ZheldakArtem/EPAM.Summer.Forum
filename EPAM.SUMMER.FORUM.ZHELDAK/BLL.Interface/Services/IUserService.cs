using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;
using BLL.Interface.Services;
namespace BLL.Interface.Services
{
    /// <summary>
    /// The interface provides methods for working with users.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="user">Instance of question.</param>
        void CreateUser(User user);

        /// <summary>
        /// Update user.
        /// </summary>
        /// <param name="user">Instance of user.</param>
        void UpdateUser(User user);

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="userId">Unique identifier of user.</param>
        void DeleteUser(int userId);
        
        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>Collection of users.</returns>
        IEnumerable<User> GetAllUsers();

        /// <summary>
        /// Get user by e-mail.
        /// </summary>
        /// <param name="email">E-mail of user.</param>
        /// <returns>Instance of user</returns>
        User GetByEmail(string email);

        /// <summary>
        /// Get question by id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Instance of question.</returns>
        User GetUserById(int userId);
    }
}
