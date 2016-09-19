using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.CustomActionMethodSelector;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers;
using EPAM.SUMMER.FORUM.ZHELDAK.Providers;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IQuestionService _questionService;
        private readonly ICommentService _commentService;
        public AdminController(ICategoryService categoryService, IUserService userService, IRoleService roleService, IQuestionService questionService, ICommentService commentService)
        {
            _roleService = roleService;
            _categoryService = categoryService;
            _userService = userService;
            _questionService = questionService;
            _commentService = commentService;
        }
        public ActionResult Admin()
        {
            var categories = _categoryService.GetAllCategories().Select(c => c.ToCategoryViewModel());
            return View("~/Views/Admin/NonAjaxView/Categories.cshtml", categories);
        }

        #region Accept Ajax
        [AcceptAjax]
        public ActionResult CategoriesForAdmin()
        {
            var categories = _categoryService.GetAllCategories().Select(c => c.ToCategoryViewModel());
            return PartialView("PartialCategories", categories);
        }

        [AcceptAjax]
        public ActionResult UsersForAdmin()
        {
            var role = _roleService.GetAll().First(r => r.Name == "admin");
            var users = _userService.GetAllUsers().Where(u => !u.Roles.Contains(role)).Select(u => u.ToUserForAdmin(_roleService));

            return PartialView("PartialUsers", users);
        }

        [AcceptAjax]
        public ActionResult QuestionForAdmin()
        {
            var questions = _questionService.GetAllQuestions().Select(q => q.ToQuestionForAdmin());

            return PartialView("PartialQuestionsForAdmin", questions);
        }

        [AcceptAjax]
        public ActionResult CommentForAdmin()
        {
            var comment = _commentService.GetAllComments().Select(c => c.ToCommentForAdminModel());
            return PartialView("PartialCommentForAdmin", comment);
        }
        #endregion

        #region Non-Ajax
        [ActionName("CategoriesForAdmin")]
        public ActionResult NonAjaxCategoriesForAdmin()
        {
            var categories = _categoryService.GetAllCategories().Select(c => c.ToCategoryViewModel());
            return View("PartialCategories", categories);
        }

        [ActionName("UsersForAdmin")]
        public ActionResult NonAjaxUsersForAdmin()
        {
            var role = _roleService.GetAll().First(r => r.Name == "admin");
            var users = _userService.GetAllUsers().Where(u => !u.Roles.Contains(role)).Select(u => u.ToUserForAdmin(_roleService));

            return View("~/Views/Admin/NonAjaxView/Users.cshtml", users);
        }

        [ActionName("QuestionForAdmin")]
        public ActionResult NonAjaxQuestionForAdmin()
        {

            return PartialView("PartialQuestionsForAdmin", _questionService.GetAllQuestions().Select(q => q.ToQuestionForAdmin()));
        }

        [ActionName("CommentForAdmin")]
        public ActionResult NonAjaxCommentForAdmin()
        {
            var comment = _commentService.GetAllComments().Select(c => c.ToCommentForAdminModel());
            return PartialView("PartialCommentForAdmin", comment);
        }
        #endregion

        [HttpGet]
        public ActionResult EditRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _userService.GetUserById((int)id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user.ToUserForAdmin(_roleService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(UserForAdminModel userForAdminModel)
        {
            if (ModelState.IsValid)
            {
                ((CustomRoleProvider)Roles.Provider).AddUserToRoles(userForAdminModel.UserId, userForAdminModel.Roles);
                return RedirectToAction("Admin");
            }
            return View(userForAdminModel);
        }


    }
}