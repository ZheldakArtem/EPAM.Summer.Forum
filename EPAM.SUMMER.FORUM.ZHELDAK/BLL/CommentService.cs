﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using ORM;

namespace BLL
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Comment> _commentRepository;

        public CommentService(IUnitOfWork uow, IRepository<Comment> commentRepository)
        {
            _uow = uow;
            _commentRepository = commentRepository;
        }
        public void CreateComment(Comment comment)
        {
            _commentRepository.Create(comment);
            _uow.Commit();
        }

        public void DeleteComment(int commentId)
        {
            _commentRepository.Delete(commentId);
            _uow.Commit();
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _commentRepository.GetAll();
        }

        public IEnumerable<Comment> GetCommentsOfUser(int userId)
        {
            var comments = _commentRepository.GetAll().Where(c => c.UserId == userId);

            return comments;
        }

        public IEnumerable<Comment> GetCommentsByQuestionId(int questionId)
        {
            var comments = _commentRepository.GetAll().Where(c => c.QuestionId == questionId);

            return comments;
        }

        public void UpdateComment(Comment comment)
        {
            _commentRepository.Update(comment);
            _uow.Commit();
        }

        public Comment GetCommentById(int commentId)
        {
            return _commentRepository.GetById(commentId);
        }

        public void UpdateGroupComment(int questionId, int[] commentId)
        {
            var comments = GetCommentsByQuestionId(questionId).ToArray();

            if (ReferenceEquals(commentId, null))
            {
                for (int i = 0; i < comments.Length; i++)
                {
                    comments[i].IsRight = false;
                    UpdateComment(comments[i]);
                }
                return;
            }

            for (int i = 0; i < comments.Length; i++)
            {
                for (int j = 0; j < commentId.Length; j++)
                {
                    if (comments[i].Id == commentId[j])
                    {
                        comments[i].IsRight = true;
                        break;
                    }
                    comments[i].IsRight = false;
                }
            }

        }
    }
}
