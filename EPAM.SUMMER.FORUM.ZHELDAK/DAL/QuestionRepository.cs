using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using ORM;

namespace DAL
{
    public class QuestionRepository : IRepository<Question>
    {
        private readonly EntityModel _context;
        public QuestionRepository(IUnitOfWork uow)
        {
            _context = uow.Context;
        }
        public void Create(Question question)
        {
            _context.Set<Question>().Add(question);
        }

        public void Update(Question question)
        {
            var upQuestion = _context.Set<Question>().FirstOrDefault(q => q.Id == question.Id);
            if (ReferenceEquals(upQuestion, null))
                throw new ArgumentException("The question isn't exist.");

            upQuestion.Question_ = question.Question_;
        }

        public void Delete(int questionId)
        {
            var delQuesrion = _context.Set<Question>().FirstOrDefault(q => q.Id == questionId);
            if (ReferenceEquals(delQuesrion, null))
                throw new ArgumentException("The question isn't exist.");

            _context.Set<Question>().Remove(delQuesrion);
        }

        public IEnumerable<Question> GetAll()
        {
            return _context.Set<Question>();
        }

        public Question GetById(int id)
        {
            return _context.Set<Question>().FirstOrDefault(q => q.Id == id); ;
        }

        public Question GetByPredicate(Expression<Func<Question, bool>> fun)
        {
            return _context.Set<Question>().FirstOrDefault(fun); ;
        }
    }
}
