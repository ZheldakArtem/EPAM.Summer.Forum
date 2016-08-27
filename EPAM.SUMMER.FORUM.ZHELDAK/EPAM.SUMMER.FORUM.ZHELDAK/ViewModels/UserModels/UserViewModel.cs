using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Enter your First Name")]
        [Display(Name = "Enter your First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter your Last Name")]
        [Display(Name = "Enter your Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Enter your birthday")]
        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "The field can not be empty!")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Incorrected email")]
        [Display(Name = "Enter your e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter your password")]
        [StringLength(50, ErrorMessage = "The password must contain at least {0} characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Enter your password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public byte[] Photo { get; set; }
    }
}
