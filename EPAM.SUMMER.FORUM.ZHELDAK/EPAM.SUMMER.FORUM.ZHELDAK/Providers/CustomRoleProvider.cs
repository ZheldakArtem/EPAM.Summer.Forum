using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;

using ORM;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService
             => (IUserService)DependencyResolver.Current.GetService(typeof(IUserService));
        public IRoleService RoleService
            => (IRoleService)DependencyResolver.Current.GetService(typeof(IRoleService));
        public override bool IsUserInRole(string email, string roleName)
        {
            var user = UserService.GetByEmail(email);
            var role = RoleService.GetAll().FirstOrDefault(r => r.Name == roleName);

            if (user == null || role == null) return false;

            if (user.Roles.Contains(role))
                return true;

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {

            var user = UserService.GetByEmail(email);
            if (ReferenceEquals(user, null))
                throw new ArgumentException("The user with the mail does't exist ");

            return user.Roles.Select(c => c.Name).ToArray(); ;
        }

        public override void CreateRole(string roleName)
        {
            var role = new Role { Name = roleName };
            RoleService.Add(role);
        }

        public void AddUserToRoles(int userId, string[] roleNames)
        {

            var user = UserService.GetUserById(userId);
            user.Roles.Clear();

            if (ReferenceEquals(roleNames, null))
            {
                user.Roles.Add(RoleService.GetByName("user"));
                return;
            }

            foreach (var roleName in roleNames)
            {
                var role = RoleService.GetAll().FirstOrDefault(r => r.Name == roleName.ToLower());
                user.Roles.Add(role);
            }

            UserService.UpdateUser(user);
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {

            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}