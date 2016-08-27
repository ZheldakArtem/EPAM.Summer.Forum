using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.QuestionModels
{
    public class QuestionForAdminModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Question { get; set; }
        public DateTime? DateOfQuestion { get; set; }
    }
}