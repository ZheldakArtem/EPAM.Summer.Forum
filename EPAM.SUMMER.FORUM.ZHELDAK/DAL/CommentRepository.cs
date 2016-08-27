using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.Repository;
using ORM;

namespace DAL
{
    public class CommentRepository : IRepository<Comment>
    {
        private readonly EntityModel _context;

        public CommentRepository(IUnitOfWork uow)
        {
            _context = uow.Context;
        }

        public void Delete(int commentId)
        {
            var delComment = _context.Set<Comment>().FirstOrDefault(c => c.Id == commentId);
            if (ReferenceEquals(delComment, null))
                throw new ArgumentException("The comment isn't exist");

            _context.Set<Comment>().Remove(delComment);
        }

        public void Update(Comment comment)
        {
            var upComment = _context.Set<Comment>().FirstOrDefault(c => c.Id == comment.Id);
            if (ReferenceEquals(upComment, null))
                throw new ArgumentException("The user isn't exist");

            upComment.Comment_ = comment.Comment_;
            upComment.IsRight = comment.IsRight;
        }

        public IEnumerable<Comment> GetAll()
        {
            return _context.Set<Comment>();
        }

        public Comment GetById(int id)
        {
            return _context.Set<Comment>().FirstOrDefault(c => c.Id == id);
        }

        public Comment GetByPredicate(Expression<Func<Comment, bool>> fun)
        {
            return _context.Set<Comment>().FirstOrDefault(fun);
        }

        public void Create(Comment comment)
        {
            _context.Set<Comment>().Add(comment);
        }
    }
}
