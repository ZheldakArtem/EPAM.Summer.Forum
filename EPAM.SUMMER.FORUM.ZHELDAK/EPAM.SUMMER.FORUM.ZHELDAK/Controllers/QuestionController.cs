using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels;
using ORM;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Pagination;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IUserService _userService;
        public QuestionController(IQuestionService serviceQuestion, IUserService userService)
        {
            _questionService = serviceQuestion;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult ShowQuestion(int categoryId, string categoryName, int page = 1)
        {
            int pageSize = 3;
            var questions = _questionService.GetQuestionsByCategory(categoryId).Select(q => q.ToQuestionViewModel());

            ViewBag.Category = categoryName;
            ViewBag.CategoryId = categoryId;
            
            IEnumerable<QuestionViewModel> questionPerPages = questions.Skip((page - 1)*pageSize).Take(pageSize);
            PageInfo pageInfo = new PageInfo {PageNumber = page, PageSize = pageSize, TotalItems = questions.Count()};
            IndexViewModel<QuestionViewModel> ivm= new IndexViewModel<QuestionViewModel> {PageInfo = pageInfo,Entities= questionPerPages };
            
            return View(ivm);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CommentsOnQuestion(int questionId)
        {
            ViewBag.Question = _questionService.GetQuestionById(questionId).ToQuestionViewModel().Question;
            ViewBag.QuestionId = questionId;
            ViewBag.UserId = _userService.GetByEmail(HttpContext.User.Identity.Name).Id;

            return View(GetCommentsOnQuestion(questionId));
        }


        private IEnumerable<CommentsOnQuestionModel> GetCommentsOnQuestion(int questionId)
        {
            var comments = _questionService.GetQuestionById(questionId).Comments.Select(c => new CommentsOnQuestionModel()
            {
                IdComment = c.Id,
                IsRight = c.IsRight,
                FirstName = c.User.FirstName,
                LastName = c.User.LastName,
                Photo = c.User.Photo,
                Comment = c.Comment_,
                DateOfComment = c.DataOfComment
            });

            return comments;
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var question = _questionService.GetQuestionById((int)id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question.ToQuestionViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _questionService.DeleteQuestion(id);

            return RedirectToAction("Admin", "Admin");
        }
    }
}