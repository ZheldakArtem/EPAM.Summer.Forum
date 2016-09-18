using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.CategoryModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Remote("CheckCategory", "Category", ErrorMessage = "Category already exists.")]
        [Required(ErrorMessage = "Enter the name of category")]
        [DisplayName("Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Enter the description of category")]
        [DisplayName("Category description")]
        public string Description { get; set; }
    }
}