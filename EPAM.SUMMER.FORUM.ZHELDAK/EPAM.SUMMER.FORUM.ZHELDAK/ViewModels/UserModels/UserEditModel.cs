using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.UserModels
{
    public class UserEditModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "The field can't be empty.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The field can't be empty.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = " Enter your birthday.")]
        [Remote("CheckBirthday", "Account", ErrorMessage = "Incorrect birthday.")]
        public string Birthday { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Remote("CheckOldPassword", "User", ErrorMessage = "Incorrect old password.")]
        [Required(ErrorMessage = "Enter your old password.")]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [StringLength(50, ErrorMessage = "The password must contain at least 6 characters", MinimumLength = 6)]
        [Required(ErrorMessage = "Enter your new password.")]
        public string NewPassword { get; set; }

        public byte[] Photo { get; set; }
        public string MimeType { get; set; }
    }
}