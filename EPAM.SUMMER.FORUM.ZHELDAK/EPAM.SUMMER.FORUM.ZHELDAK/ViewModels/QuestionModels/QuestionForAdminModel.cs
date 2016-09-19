using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.QuestionModels
{
    public class QuestionForAdminModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        [StringLength(1000, ErrorMessage = "The question should contain from 6 to 1000 characters.", MinimumLength = 6)]
        public string Question { get; set; }
        public DateTime? DateOfQuestion { get; set; }
    }
}