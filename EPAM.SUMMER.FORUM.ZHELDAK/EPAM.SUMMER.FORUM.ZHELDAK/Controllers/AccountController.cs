using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers;
using EPAM.SUMMER.FORUM.ZHELDAK.Providers;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private const int Maxyears = 100;
        public AccountController(IUserService service)
        {
            _userService = service;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
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
        public ActionResult Register(UserRegisterViewModel models, HttpPostedFileBase uploadImage)
        {
            var user = _userService.GetAllUsers().Any(u => u.Email.Equals(models.Email));
            if (user)
            {
                ModelState.AddModelError("", "User whith this address already registered.");
                return View(models);
            }

            if (models.Birthday > DateTime.Now || models.Birthday < DateTime.Now.AddYears(-60))
            {
                ModelState.AddModelError("", "Incorrect date birthday.");
                return View(models);
            }

            if (ModelState.IsValid)
            {
                SelectPhoto(models, uploadImage);

                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                     .CreateUser(models.ToUser());
                if (membershipUser != null)
                {
                    FormsAuthentication.SetAuthCookie(models.Email, false);
                    return RedirectToAction("MainPage", "Category");
                }
                ModelState.AddModelError("", "Error registration.");

                return View(models);
            }

            return View(models);
        }

        public ActionResult AccountPage()
        {
            var user = _userService.GetByEmail(HttpContext.User.Identity.Name);
            return View(user.ToUserForAccountModel());
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult CheckEmail(string email)
        {
            var anyUser = _userService.GetAllUsers().Any(u => u.Email.Equals(email));

            return Json(!anyUser, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpGet]
        public JsonResult CheckBirthday(string birthday)
        {
            DateTime parsedDate;

            if (!DateTime.TryParse(birthday, out parsedDate))
            {
                return Json("Incorrect date format.(dd.mm.yyyy)",
                    JsonRequestBehavior.AllowGet);
            }
            if (parsedDate < DateTime.Now.AddYears(-Maxyears) || parsedDate > DateTime.Now)
            {
                return Json("Incorrect date.",
                    JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// The method sets the user's photos.
        /// </summary>
        private void SelectPhoto(UserRegisterViewModel models, HttpPostedFileBase uploadImage)
        {
            byte[] imageData;
            if (ReferenceEquals(uploadImage, null))
            {
                var filePath = Server.MapPath("~/fonts/NoPhoto.jpg");
                using (Stream fstream = System.IO.File.OpenRead(filePath))
                {
                    using (var binaryReader = new BinaryReader(fstream))
                    {
                        imageData = binaryReader.ReadBytes((int)fstream.Length);
                    }
                }
                models.MimeType = "image/jpeg";
            }
            else
            {
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                models.MimeType = uploadImage.ContentType;
            }

            models.Photo = imageData;
        }
    }
}