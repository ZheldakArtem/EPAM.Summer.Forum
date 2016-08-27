using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels
{
    public class CommentsOnQuestionModel
    {
        public int IdComment { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }
        public bool? IsRight { get; set; }
        public string Comment { get; set; }
        public DateTime? DateOfComment { get; set; }
    }
}