using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using ORM;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.UserModels
{
    public class UserForAccountModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public DateTime Birthday { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }
        public ICollection<Question> Questions { get; set; }

    }
}