using DAL.Interface.Repository;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using BLL.Interface;
using BLL.Interface.Services;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers;
using EPAM.SUMMER.FORUM.ZHELDAK.Providers;
using ORM;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService service)
        {
            _userService = service;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            var type = HttpContext.User.GetType();
            var iden = HttpContext.User.Identity.GetType();
            ViewBag.ReturnUrl = returnUrl;

            return View();
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("MainPage", "Category");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LogOnViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid && Membership.ValidateUser(viewModel.Email, viewModel.Password))
            {
                FormsAuthentication.SetAuthCookie(viewModel.Email, viewModel.RememberMe);
                if (Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);
                return RedirectToAction("MainPage", "Category");
            }
            ModelState.AddModelError("", "Incorrect login or password");

            return View(viewModel);
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(UserViewModel models, HttpPostedFileBase uploadImage)
        {
            var anyUser = _userService.GetAllUsers().Any(u => u.Email.Contains(models.Email));

            if (anyUser)
            {
                ModelState.AddModelError("", "User whith this address already registered.");
                return View(models);
            }

            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                //read the transferred file in byte array
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // installation of an array of bytes
                models.Photo = imageData;
                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                     .CreateUser(models.ToUser());
                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(models.Email, false);
                    return RedirectToAction("MainPage", "Category");
                }
                else
                {
                    ModelState.AddModelError("", "Error registration.");
                }
                return View(models);
            }

            return View(models);
        }


    }
}