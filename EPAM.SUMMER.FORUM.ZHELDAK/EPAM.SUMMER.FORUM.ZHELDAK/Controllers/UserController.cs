using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using BLL.Interface.Services;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers;
using EPAM.SUMMER.FORUM.ZHELDAK.Providers;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.UserModels;
using ORM;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public ActionResult Edit()
        {
            var user = _userService.GetByEmail(User.Identity.Name);

            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.ToUserEditModel());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserEditModel models, HttpPostedFileBase uploadImage)
        {

            byte[] imageData;

            if (uploadImage != null)
            {
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                models.MimeType = uploadImage.ContentType;
                models.Photo = imageData;
            }
           
            models.Email = User.Identity.Name;
            models.NewPassword = Crypto.HashPassword(models.NewPassword);
            _userService.UpdateUser(models.ToUser());

            return RedirectToAction("AccountPage", "Account");
        }


        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    User user = db.Users.Find(id);
        //    if (user == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(user);
        //}


        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    User user = db.Users.Find(id);
        //    db.Users.Remove(user);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        public ActionResult CheckEmail(string email)
        {
            if (email == User.Identity.Name)
                return Json(true, JsonRequestBehavior.AllowGet);

            if (_userService.GetAllUsers().Any(u => u.Email.Equals(email)))
                return Json(false, JsonRequestBehavior.AllowGet);

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CheckOldPassword(string oldPassword)
        {
            var user = _userService.GetByEmail(User.Identity.Name);

            if (Crypto.VerifyHashedPassword(user.Password, oldPassword))
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPhoto(int userId)
        {
            var user = _userService.GetUserById(userId);

            if (user != null)
            {
                return File(user.Photo,user.MimeType);
            }

            return null;
        }
    }
}
