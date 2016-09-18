using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels
{
    public class CommentViewModel
    {
        public int? Id { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int QuestionId { get; set; }
    }
}