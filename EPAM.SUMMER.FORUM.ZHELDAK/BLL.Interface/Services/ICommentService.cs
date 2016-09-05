﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace BLL.Interface.Services
{
    public interface ICommentService
    {
        void CreateComment(Comment comment);
        void DeleteComment(int commentId);
        void UpdateComment(Comment comment);
        IEnumerable<Comment> GetCommentsOfUser(int userId);
        IEnumerable<Comment> GetCommentsByQuestionId(int questionId);
        IEnumerable<Comment> GetAllComments();
        Comment GetCommentById(int commentId);
        void UpdateGroupComment(int questionId,int[] commentId);
    }
}
