using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.Repository;
using DAL.NLog;
using ORM;

namespace DAL
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly ILogForum _logForum;
        private readonly EntityModel _context;

        public CommentRepository(IUnitOfWork uow, ILogForum logForum)
        {
            _logForum = logForum;
            _context = uow.Context;
        }

        public void Create(Comment comment)
        {
            _logForum.Info($"Create comment => {comment.Comment_}. | {DateTime.Now}");
            _context.Set<Comment>().Add(comment);
        }

        public void Update(Comment comment)
        {
            var upComment = _context.Set<Comment>().FirstOrDefault(c => c.Id == comment.Id);
            if (ReferenceEquals(upComment, null))
            {
                var ex = new ArgumentException("The comment isn't exist");
                _logForum.Error(ex, $"{ex.Message} | {DateTime.Now}");

                throw ex;
            }

            upComment.Comment_ = comment.Comment_;
            upComment.IsRight = comment.IsRight;
            _logForum.Info($"Update comment. | {DateTime.Now}");
        }

        public void Delete(int commentId)
        {
            var delComment = _context.Set<Comment>().FirstOrDefault(c => c.Id == commentId);
            if (ReferenceEquals(delComment, null))
            {
                var ex = new ArgumentException("The comment isn't exist");
                _logForum.Error(ex, $"{ex.Message} | {DateTime.Now}");

                throw ex;
            }

            _context.Set<Comment>().Remove(delComment);
            _logForum.Info($"Delete comment => {delComment.Comment_}. | {DateTime.Now}");
        }
        
        public IEnumerable<Comment> GetAll()
        {
            _logForum.Info($"Get all comments. | {DateTime.Now}");
            return _context.Set<Comment>();
        }

        public Comment GetById(int id)
        {
            _logForum.Info($"Get comment by id={id}. | {DateTime.Now}");
            return _context.Set<Comment>().FirstOrDefault(c => c.Id == id);
        }

        public Comment GetByPredicate(Expression<Func<Comment, bool>> fun)
        {
            _logForum.Info($"Get comment by predicate => {fun}. | {DateTime.Now}");
            return _context.Set<Comment>().FirstOrDefault(fun);
        }
    }
}
