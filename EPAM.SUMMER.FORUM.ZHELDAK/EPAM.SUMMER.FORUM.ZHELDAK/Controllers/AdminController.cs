using System;
using System.Collections.Generic;
using System.Drawing.Design;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers;
using EPAM.SUMMER.FORUM.ZHELDAK.Providers;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.QuestionModels;
using Ninject.Infrastructure.Language;
using ORM;

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
        public AdminController(ICategoryService categoryService,IUserService userService,IRoleService roleService, IQuestionService questionService,ICommentService commentService)
        {
            _roleService = roleService;
            _categoryService = categoryService;
            _userService = userService;
            _questionService = questionService;
            _commentService = commentService;
        }
        public ActionResult Admin()
        {
            return View("AdminPage");
        }

        [ChildActionOnly]
        public ActionResult CategoriesForAdmin()
        {
            var categories = _categoryService.GetAllCategories().Select(c => c.ToCategoryViewModel());
            return PartialView("PartialCategories", categories);
        }

        [ChildActionOnly]
        public ActionResult UsersFodAdmin()
        {
            var role = _roleService.GetAll().First(r => r.Name == "admin");
            var users = _userService.GetAllUsers().Where(u=>!u.Roles.Contains(role)).Select(u => u.ToUserForAdmin(_roleService));

            return PartialView("PartialUsers", users);
        }

        [HttpGet]
        public ActionResult EditRole(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            var user = _userService.GetUser((int)id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return  View(user.ToUserForAdmin(_roleService));
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

        [ChildActionOnly]
        public ActionResult QuestionForAdmin()
        {
           
            return PartialView("PartialQuestions", _questionService.GetAllQuestions().Select(q => q.ToQuestionForAdmin()));
        }

        public ActionResult CommentForAdmin()
        {
            var comment = _commentService.GetAllComments().Select(c=>c.ToCommentForAdminModel());
            return PartialView("PartialCommentForAdmin", comment);
        }
    }
}