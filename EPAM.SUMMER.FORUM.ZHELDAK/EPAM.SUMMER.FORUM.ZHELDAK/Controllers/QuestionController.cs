using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Common;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Pagination;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly IUserService _userService;
        private readonly ICategoryService _categoryService;
        public QuestionController(IQuestionService serviceQuestion, IUserService userService, ICategoryService categoryService)
        {
            _categoryService = categoryService;
            _questionService = serviceQuestion;
            _userService = userService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ShowQuestion(string categoryName, int page = 1)
        {
            var pageSize = Constants.ItemsPerPage;
            var questions = _questionService
                .GetQuestionsByCategory(categoryName)
                .Select(q => q.ToQuestionViewModel());

            ViewBag.Category = categoryName.ToUpper();

            var questionPerPages = questions.Skip((page - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = questions.Count()
            };
            var ivm = new IndexViewModel<QuestionViewModel>
            {
                PageInfo = pageInfo,
                Entities = questionPerPages
            };

            if (Request.IsAjaxRequest())
            {
                return PartialView("PartialShowQuestion", ivm);
            }
            return View(ivm);
        }

        [HttpGet]
        public ActionResult CommentsOnQuestion(int questionId)
        {
            ViewBag.Question = _questionService
                .GetQuestionById(questionId)
                .ToQuestionViewModel()
                .Question;
            ViewBag.QuestionId = questionId;
            ViewBag.Sender = _questionService.GetQuestionById(questionId).User;
            ViewBag.CurrentUser = _userService.GetByEmail(User.Identity.Name);

            return View(GetCommentsOnQuestion(questionId));
        }

        private IEnumerable<CommentsOnQuestionModel> GetCommentsOnQuestion(int questionId)
        {
            var comments = 
                _questionService
                .GetQuestionById(questionId)
                .Comments
                .Select(c => c.ToCommentOnQuestionModel());

            return comments;
        }

        [HttpGet]
        public ActionResult Edit(int? id)
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
            var categories = _categoryService.GetAllCategories();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            return View(question.ToQuestionViewModel());
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed(QuestionViewModel questionViewModel)
        {
            if (ModelState.IsValid)
            {
                _questionService.UpdateQuestion(questionViewModel.ToQuestion());

                if (HttpContext.User.Identity.Name == "admin")
                    return RedirectToAction("Admin", "Admin");

                return RedirectToAction("AccountPage", "Account");
            }
            return View(questionViewModel);
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
            if (Request.IsAjaxRequest())
            {
                _questionService.DeleteQuestion((int)id);
                var newList = _questionService
                    .GetAllQuestions()
                    .Select(q => q.ToQuestionForAdmin());

                return PartialView("PartialQuestionsForAdmin", newList);
            }
            return View(question.ToQuestionViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                _questionService.DeleteQuestion(id);

                if (HttpContext.User.Identity.Name == "admin")
                    return RedirectToAction("Admin", "Admin");

                return RedirectToAction("AccountPage", "Account");
            }
            return View();
        }

        public ActionResult Create()
        {
            var categories = _categoryService.GetAllCategories();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestionViewModel questionViewModel)
        {
            var user = _userService.GetByEmail(User.Identity.Name);
            if (ModelState.IsValid)
            {
                questionViewModel.UserId = user.Id;
                _questionService.CreateQuestion(questionViewModel.ToQuestion());

                return RedirectToAction("AccountPage", "Account");
            }

            var categories = _categoryService.GetAllCategories();
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");

            return View(questionViewModel);
        }
    }
}