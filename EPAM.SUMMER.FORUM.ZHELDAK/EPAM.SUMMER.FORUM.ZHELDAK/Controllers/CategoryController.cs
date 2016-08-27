using ORM;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Interface.Services;
using EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Controllers
{
    [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService service)
        {
            _categoryService = service;
        }

        [AllowAnonymous]
        public ActionResult MainPage()
        {
            var categories = _categoryService.GetAllCategories().Select(c => c.ToCategoryViewModel());

            return View(categories);
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
            CategoryViewModel category = _categoryService.GetById((int)id).ToCategoryViewModel();
            if (category == null)
            {
                return HttpNotFound();
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
            return View(category.ToCategoryViewModel());
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _categoryService.DeleteCategory(id);

            return RedirectToAction("Admin", "Admin");
        }

       
    }
}