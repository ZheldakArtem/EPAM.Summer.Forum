using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Enter your First Name.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter your Last Name.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = " Enter your birthday.")]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        [Remote("CheckBirthday", "Account", ErrorMessage = "Incorrect birthday.")]
        public DateTime Birthday { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrected email.")]
        [Remote("CheckEmail", "Account", ErrorMessage = "User whith this address already registered.")]
        [Display(Name = "Enter your e-mail")]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage = "The password must contain at least 6 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        [Required(ErrorMessage = "Enter your password.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

        public byte[] Photo { get; set; }

        public string MimeType { get; set; }
    }
}
