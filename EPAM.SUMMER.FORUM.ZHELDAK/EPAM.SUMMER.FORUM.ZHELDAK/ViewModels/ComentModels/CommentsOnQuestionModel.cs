using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels
{
    public class CommentsOnQuestionModel
    {
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsRight { get; set; }
        public string Comment { get; set; }
        public DateTime? DateOfComment { get; set; }
    }
}