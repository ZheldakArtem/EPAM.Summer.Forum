using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels
{
    public class QuestionViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter the question")]
        [DisplayName("Enter question")]
        public string Question { get; set; }
        public DateTime? LastComment { get; set; }
        public int CommentsCount { get; set; }
        public DateTime? DateOfQuestion { get; set; }
    }
}