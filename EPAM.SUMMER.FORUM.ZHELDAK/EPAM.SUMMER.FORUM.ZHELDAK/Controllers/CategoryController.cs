using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Common;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Pagination;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.CategoryModels;


namespace EPAM.SUMMER.FORUM.ZHELDAK.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        [AllowAnonymous]
        public ActionResult MainPage(int page = 1)
        {
            var pageSize = Constants.ItemsPerPage;
            var categories = _categoryService.GetAllCategories().Select(c => c.ToCategoryViewModel());
            var categoryPerPages = categories.Skip((page - 1) * pageSize).Take(pageSize);
            var pageInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = categories.Count() };
            var ivm = new IndexViewModel<CategoryViewModel> { PageInfo = pageInfo, Entities = categoryPerPages };

            if (Request.IsAjaxRequest())
            {
                return PartialView("PartialShowCategory", ivm);
            }

            return View(ivm);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Search(string prefix)
        {
            var categories = _categoryService.GetAllCategories().Where(c => c.Name.Contains(prefix.ToUpper())).Select(c => new { c.Name });
            return Json(categories, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.CreateCategory(category.ToCategory());

                return RedirectToAction("Admin", "Admin");
            }

            return View(category);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _categoryService.GetById((int)id).ToCategoryViewModel();

            if (category == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("PartialEdit", category);
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                _categoryService.UpdateCategory(category.ToCategory());
                return RedirectToAction("Admin", "Admin");
            }
            return View(category);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var category = _categoryService.GetById((int)id);

            if (category == null)
            {
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                _categoryService.DeleteCategory((int)id);

                var newList = _categoryService.GetAllCategories().Select(c => c.ToCategoryViewModel());

                return PartialView("PartialCategories", newList);
            }

            return View(category.ToCategoryViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoryService.DeleteCategory(id);

            return RedirectToAction("Admin", "Admin");
        }

        public ActionResult CheckCategory(string name)
        {
            var user = _categoryService.GetAllCategories().Any(c => c.Name.Equals(name.ToUpper()));

            if (!user)
                return Json(true, JsonRequestBehavior.AllowGet);

            return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}