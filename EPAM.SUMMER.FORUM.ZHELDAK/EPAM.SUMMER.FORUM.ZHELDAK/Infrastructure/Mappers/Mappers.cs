using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Services;
using ORM;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.QuestionModels;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers
{
    public static class Mappers
    {
        public static User ToUser(this UserViewModel viewModel)
        {
            return new User
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Birthday = viewModel.Birthday,
                Password = viewModel.Password,
                Email = viewModel.Email,
                Photo = viewModel.Photo
            };
        }

        public static UserViewModel ToUserViewModel(this User user)
        {
            return new UserViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Password = user.Password,
                Email = user.Email,
                Photo = user.Photo
            };
        }

        public static Category ToCategory(this CategoryViewModel categoryViewModel)
        {
            return new Category()
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name,
                Description = categoryViewModel.Description
            };
        }

        public static CategoryViewModel ToCategoryViewModel(this Category category)
        {
            return new CategoryViewModel()
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public static QuestionViewModel ToQuestionViewModel(this Question question)
        {
            var lastComments = question.Comments.Max(c => c.DataOfComment);

            return new QuestionViewModel()
            {
                Id = question.Id,
                Question = question.Question_,
                DateOfQuestion = question.DateOfQuestion,
                CommentsCount = question.Comments.Count,
                LastComment = lastComments
            };
        }

        public static Comment ToComment(this CommentViewModel commentViewModel)
        {
            return new Comment()
            {
                Comment_ = commentViewModel.Comment,
                DataOfComment = DateTime.Now,
                UserId = commentViewModel.UserId,
                QuestionId = commentViewModel.QuestionId
            };
        }

        public static UserForAdminModel ToUserForAdmin(this User user, IRoleService role)
        {
            return new UserForAdminModel()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = role.GetAll().Select(r => new string(r.Name.ToCharArray())).ToArray()
            };
        }

        public static QuestionForAdminModel ToQuestionForAdmin(this Question question)
        {
            return new QuestionForAdminModel()
            {
                Id = question.Id,
                CategoryName = question.Category.Name,
                DateOfQuestion = question.DateOfQuestion,
                Question = question.Question_
            };
        }
    }
}