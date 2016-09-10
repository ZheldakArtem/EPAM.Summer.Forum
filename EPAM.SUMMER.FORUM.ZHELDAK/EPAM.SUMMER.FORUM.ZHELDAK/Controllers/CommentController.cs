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
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.ComentModels;
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
        public ActionResult Create(CommentViewModel commentViewModel)
        {
            //var comment = new CommentViewModel()
            //{
            //    QuestionId = int.Parse(Request.Form["QuestionId"]),
            //    UserId = int.Parse(Request.Form["UserId"]),
            //    Comment = Request.Form["Comment"]
            //};

            _commentService.CreateComment(commentViewModel.ToComment());
            var newComment = _commentService.GetAllComments().Last();

            var viewComment = newComment.ToCommentOnQuestionModel();

            return PartialView("PartialComments", viewComment);
        }

        public ActionResult Edit(int? questionId)
        {
            if (questionId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.QuestionId = questionId;
            var comment = _commentService.GetCommentsByQuestionId((int)questionId);
            if (ReferenceEquals(comment, null))
            {
                return HttpNotFound();
            }
            return View(comment.Select(c => c.ToCommentForAccountModel()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int questionId, int[] commentId = null)
        {
            if (ModelState.IsValid)
            {
                _commentService.UpdateGroupComment(questionId, commentId);
                return RedirectToAction("AccountPage", "Account");
            }

            return RedirectToAction("Edit", "Comment", new { questionId });
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = _commentService.GetCommentById((int)id);
            if (ReferenceEquals(comment, null))
            {
                return HttpNotFound();
            }
            return View(comment.ToCommentForAdminModel());
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _commentService.DeleteComment(id);

            return RedirectToAction("Admin", "Admin");
        }

    }
}
