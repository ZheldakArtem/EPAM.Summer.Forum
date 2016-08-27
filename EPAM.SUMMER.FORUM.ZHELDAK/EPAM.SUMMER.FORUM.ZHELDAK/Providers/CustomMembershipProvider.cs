using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using BLL.Interface;
using DAL.Interface.Repository;
using ORM;
using BLL;
using BLL.Interface.Services;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Providers
{
    public class CustomMembershipProvider: MembershipProvider
    {
        public IUserService UserService
           => (IUserService)DependencyResolver.Current.GetService(typeof(IUserService));
        public IRoleService RoleService
            => (IRoleService)DependencyResolver.Current.GetService(typeof(IRoleService));

        public MembershipUser CreateUser(User user)
        {
            MembershipUser membershipUser = GetUser(user.Email, false);
            if (membershipUser != null)
                return null;

            var role = RoleService.GetAll().FirstOrDefault(r => r.Name == "User".ToLower());
            user.Password = Crypto.HashPassword(user.Password);
            user.Roles.Add(role);
            UserService.CreateUser(user);
            membershipUser = GetUser(user.Email, false);

            return membershipUser;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = UserService.GetByEmail(email);
            if (user == null) return null;
            var membershipUser=new MembershipUser("CustomMembershipProvider",user.Email, null, null, null, null,
                false, false, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return membershipUser;
        }

        public override bool ValidateUser(string email, string password)
        {
            var user = UserService.GetByEmail(email);
            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
                return true;

            return false;
        }

#region Not Emplemeted
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

       public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

       

      

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval { get; }
        public override bool EnablePasswordReset { get; }
        public override bool RequiresQuestionAndAnswer { get; }
        public override string ApplicationName { get; set; }
        public override int MaxInvalidPasswordAttempts { get; }
        public override int PasswordAttemptWindow { get; }
        public override bool RequiresUniqueEmail { get; }
        public override MembershipPasswordFormat PasswordFormat { get; }
        public override int MinRequiredPasswordLength { get; }
        public override int MinRequiredNonAlphanumericCharacters { get; }
        public override string PasswordStrengthRegularExpression { get; }
#endregion
    }
}