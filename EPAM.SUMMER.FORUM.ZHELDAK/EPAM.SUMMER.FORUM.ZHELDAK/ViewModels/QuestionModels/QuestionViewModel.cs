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
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        [StringLength(1000, ErrorMessage = "The question should contain from 6 to 1000 characters.", MinimumLength = 6)]
        [Required(ErrorMessage = "The field can't be empty.")]
        [DisplayName("Enter question")]
        public string Question { get; set; }

        public int CommentsCount { get; set; }

        public DateTime? LastComment { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfQuestion { get; set; }
    }
}