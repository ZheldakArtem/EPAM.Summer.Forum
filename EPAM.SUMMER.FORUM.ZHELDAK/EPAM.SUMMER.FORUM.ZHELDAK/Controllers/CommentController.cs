using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels;
using ORM;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create()
        {
            var comment = new CommentViewModel()
            {
                QuestionId = int.Parse(Request.Form["QuestionId"]),
                UserId = int.Parse(Request.Form["UserId"]),
                Comment = Request.Form["Comment"]
            };

            _commentService.CreateComment(comment.ToComment());
            var newComment = _commentService.GetAllComments().Last();

            var viewComment = new CommentsOnQuestionModel()
            {
                IdComment = newComment.Id,
                Comment = newComment.Comment_,
                DateOfComment = newComment.DataOfComment,
                FirstName = newComment.User.FirstName,
                IsRight = newComment.IsRight,
                LastName = newComment.User.LastName,
                Photo = newComment.User.Photo
            };

            return PartialView("PartialComments", viewComment);
        }
    }
}
