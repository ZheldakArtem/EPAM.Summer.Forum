using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using ORM;

namespace BLL
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IUnitOfWork uow, IRepository<Category> categoryRepository)
        {
            _uow = uow;
            _categoryRepository = categoryRepository;
        }

        public void CreateCategory(Category category)
        {
            _categoryRepository.Create(category);
            _uow.Commit();
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
            _uow.Commit();
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryRepository.Delete(categoryId);
            _uow.Commit();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetById(int categoryId)
        {
            return _categoryRepository.GetById(categoryId);
        }
    }
}
