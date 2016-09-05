using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.ComentsModels
{
    public class CommentForAdminModel
    {
        public int Id { get; set; }
        public DateTime? DateOfComment { get; set; }
        public string Comment { get; set; }
        public string Question { get; set; }
    }
}