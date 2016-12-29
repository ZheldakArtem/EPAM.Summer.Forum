using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ORM;

namespace BLL.Interface.Services
{
    /// <summary>
    /// The interface provides methods for working with roles.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Add new role.
        /// </summary>
        /// <param name="role">Instance of role.</param>
        void Add(Role role);

        /// <summary>
        /// Delete role.
        /// </summary>
        /// <param name="id">Unique identifier of role.</param>
        void Remove(int id);

        /// <summary>
        /// Edit role.
        /// </summary>
        /// <param name="role">Instance of role.</param>
        void Edit(Role role);

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns>Collection of roles.</returns>
        IEnumerable<Role> GetAll();

        /// <summary>
        /// Get role by id.
        /// </summary>
        /// <param name="id">Unique identifier of role.</param>
        /// <returns>Instance of role.</returns>
        Role GetById(int id);

        /// <summary>
        /// Get role by name.
        /// </summary>
        /// <param name="nameRole">Name of role.</param>
        /// <returns>Instance of role.</returns>
        Role GetByName(string nameRole);
    }
}
