using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interface;
using BLL.Interface.Services;
using DAL.Interface.Repository;
using ORM;

namespace BLL
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<Question> _questionRepository;

        public QuestionService(IUnitOfWork uow, IRepository<Question> questionRepository)
        {
            _uow = uow;
            _questionRepository = questionRepository;
        }
        public void CreateQuestion(Question question)
        {
            _questionRepository.Create(question);
            _uow.Commit();
        }

        public void UpdateQuestion(Question question)
        {
            _questionRepository.Update(question);
            _uow.Commit();
        }

        public void DeleteQuestion(int questionId)
        {
            _questionRepository.Delete(questionId);
            _uow.Commit();
        }

        public IEnumerable<Question> GetAllQuestions()
        {
            return _questionRepository.GetAll();
        }

        public IEnumerable<Question> GetUsersQuestions(int userId)
        {
            var questions = _questionRepository
                .GetAll()
                .Where(q => q.UserId == userId);

            return questions;
        }

        public IEnumerable<Question> GetQuestionsByCategory(int categoryId)
        {
            var questions = _questionRepository
                .GetAll()
                .Where(q => q.CategoryId == categoryId);
             

            return questions;
        }

       public Question GetQuestionById(int questionId)
        {
            return _questionRepository.GetById(questionId);
        }

        public IEnumerable<Question> GetQuestionsByCategory(string categoryName)
        {
            var questions = _questionRepository
                .GetAll()
                .Where(q => q.Category.Name.Equals(categoryName.ToUpper()));
             
            return questions;
        }
    }
}
