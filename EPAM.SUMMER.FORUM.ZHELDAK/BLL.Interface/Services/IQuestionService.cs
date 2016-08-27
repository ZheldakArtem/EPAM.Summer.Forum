using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace BLL.Interface.Services
{
    public interface IQuestionService
    {
        void CreateQuestion(Question question);
        void UpdateQuestion(Question question);
        void DeleteQuestion(int questionId);
        IEnumerable<Question> GetAllQuestions();
        IEnumerable<Question> GetUsersQuestions(int userId);
        IEnumerable<Question> GetQuestionsByCategory(int categoryId);
        Question GetQuestionById(int questionId);
        
    }
}
