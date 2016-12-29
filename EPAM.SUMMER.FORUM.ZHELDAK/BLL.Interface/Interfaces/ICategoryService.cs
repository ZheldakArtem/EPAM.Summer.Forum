using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace BLL.Interface.Services
{
    /// <summary>
    /// The interface provides methods for working with categories.
    /// </summary>
    public interface ICategoryService
    {
        /// <summary>
        /// Create new category.
        /// </summary>
        /// <param name="category">Instance of category.</param>
        void CreateCategory(Category category);

        /// <summary>
        /// Delete category.
        /// </summary>
        /// <param name="categoryId">Unique identifier of category.</param>
        void DeleteCategory(int categoryId);

        /// <summary>
        /// Update category.
        /// </summary>
        /// <param name="category">Instance of category.</param>
        void UpdateCategory(Category category);

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>Collection of categories.</returns>
        IEnumerable<Category> GetAllCategories();

        /// <summary>
        /// Get category by id.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>Instance of category.</returns>
        Category GetById(int categoryId);
    }
}
