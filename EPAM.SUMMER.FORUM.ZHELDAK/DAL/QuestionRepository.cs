using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Repository;
using DAL.NLog;
using ORM;

namespace DAL
{
    public class QuestionRepository : IRepository<Question>
    {
        private readonly EntityModel _context;
        private readonly ILogForum _logForum;
        public QuestionRepository(IUnitOfWork uow,ILogForum logForum)
        {
            _logForum = logForum;
            _context = uow.Context;
        }
        public void Create(Question question)
        {
            _logForum.Info($"Create question => {question.Question_}. | {DateTime.Now}");
            _context.Set<Question>().Add(question);
        }

        public void Update(Question question)
        {
            var upQuestion = _context.Set<Question>().FirstOrDefault(q => q.Id == question.Id);
            if (ReferenceEquals(upQuestion, null))
            {
                var ex = new ArgumentException("The question isn't exist");
                _logForum.Error(ex, $"{ex.Message} | {DateTime.Now}");

                throw ex;
            }

            upQuestion.Question_ = question.Question_;
            _logForum.Info($"Delete question => {upQuestion.Question_}. | {DateTime.Now}");
        }

        public void Delete(int questionId)
        {
            var delQuestion = _context.Set<Question>().FirstOrDefault(q => q.Id == questionId);
            if (ReferenceEquals(delQuestion, null))
            {
                var ex = new ArgumentException("The question isn't exist");
                _logForum.Error(ex, $"{ex.Message} | {DateTime.Now}");

                throw ex;
            }

            _context.Set<Question>().Remove(delQuestion);
            _logForum.Info($"Delete question => {delQuestion.Question_}. | {DateTime.Now}");
        }

        public IEnumerable<Question> GetAll()
        {
            _logForum.Info($"Get all questions. | {DateTime.Now}");
            return _context.Set<Question>();
        }

        public Question GetById(int id)
        {
            _logForum.Info($"Get question by id={id}. | {DateTime.Now}");
            return _context.Set<Question>().FirstOrDefault(q => q.Id == id); ;
        }

        public Question GetByPredicate(Expression<Func<Question, bool>> fun)
        {
            _logForum.Info($"Get question by predicate =>{fun}. | {DateTime.Now}");
            return _context.Set<Question>().FirstOrDefault(fun); ;
        }
    }
}
