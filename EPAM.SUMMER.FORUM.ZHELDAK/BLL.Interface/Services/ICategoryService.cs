using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace BLL.Interface.Services
{
    public interface ICategoryService
    {
        void CreateCategory(Category category);
        void DeleteCategory(int categoryId);
        void UpdateCategory(Category category);
        IEnumerable<Category> GetAllCategories();
        Category GetById(int categoryId);
    }
}
