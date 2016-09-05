using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BLL.Interface.Services;
using ORM;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.ComentModels;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.ComentsModels;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.QuestionModels;
using EPAM.SUMMER.FORUM.ZHELDAK.ViewModels.UserModels;

namespace EPAM.SUMMER.FORUM.ZHELDAK.Infrastructure.Mappers
{
    public static class Mappers
    {
        public static User ToUser(this UserRegisterViewModel viewModel)
        {
            return new User
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                Birthday = viewModel.Birthday,
                Password = viewModel.Password,
                Email = viewModel.Email,
                Photo = viewModel.Photo,
                MimeType = viewModel.MimeType
            };
        }

        public static UserRegisterViewModel ToUserViewModel(this User user)
        {
            return new UserRegisterViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Email = user.Email,
            };
        }

        public static User ToUser(this UserEditModel user)
        {

            return new User
            {
                Id = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = DateTime.Parse(user.Birthday),
                Password = user.NewPassword,
                Email = user.Email,
                Photo = user.Photo
            };
        }

        public static UserEditModel ToUserEditModel(this User user)
        {
            return new UserEditModel()
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Birthday = user.Birthday.ToShortDateString(),
                Photo = user.Photo,
                MimeType = user.MimeType
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

        public static Question ToQuestion(this QuestionViewModel question)
        {
            return new Question
            {
                CategoryId = question.CategoryId,
                Question_ = question.Question,
                UserId = question.UserId,
                DateOfQuestion = DateTime.Now
            };
        }

        public static CommentsOnQuestionModel ToCommentOnQuestionModel(this Comment comment)
        {
            return new CommentsOnQuestionModel()
            {
                CommentId = comment.Id,
                UserId=comment.UserId,
                Comment = comment.Comment_,
                DateOfComment = comment.DataOfComment,
                FirstName = comment.User.FirstName,
                IsRight = comment.IsRight,
                LastName = comment.User.LastName,
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

        public static UserForAccountModel ToUserForAccountModel(this User user)
        {
            return new UserForAccountModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Photo = user.Photo,
                Birthday = user.Birthday,
                Questions = user.Questions
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

        public static CommentForAdminModel ToCommentForAdminModel(this Comment comment)
        {
            return new CommentForAdminModel
            {
                Id = comment.Id,
                Comment = comment.Comment_,
                DateOfComment = comment.DataOfComment,
                Question = comment.Question.Question_
            };
        }

        public static CommentForAccountModel ToCommentForAccountModel(this Comment comment)
        {
            return new CommentForAccountModel
            {
                CommentId = comment.Id,
                Comment = comment.Comment_,
                FirstName = comment.User.FirstName,
                LastName = comment.User.LastName,
                IsRight = comment.IsRight,
            };
        }

        public static Comment ToComment(this CommentForAccountModel comment)
        {
            return new Comment
            {
                Comment_ = comment.Comment,
                IsRight = comment.IsRight,
            };
        }
    }
}