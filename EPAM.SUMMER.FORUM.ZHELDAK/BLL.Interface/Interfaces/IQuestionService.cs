using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace BLL.Interface.Services
{
    /// <summary>
    /// The interface provides methods for working with questions.
    /// </summary>
    public interface IQuestionService
    {
        /// <summary>
        /// Create new question.
        /// </summary>
        /// <param name="question">Instance of question.</param>
        void CreateQuestion(Question question);

        /// <summary>
        /// Update question.
        /// </summary>
        /// <param name="question">Instance of question.</param>
        void UpdateQuestion(Question question);

        /// <summary>
        /// Delete question.
        /// </summary>
        /// <param name="questionId">Unique identifier of question.</param>
        void DeleteQuestion(int questionId);

        /// <summary>
        /// Get all questions.
        /// </summary>
        /// <returns>Collection of questions.</returns>
        IEnumerable<Question> GetAllQuestions();

        /// <summary>
        /// Get questions of user by user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Collection of questions.</returns>
        IEnumerable<Question> GetUsersQuestions(int userId);

        /// <summary>
        /// Get questions by category id.
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns>Collection of questions.</returns>
        IEnumerable<Question> GetQuestionsByCategory(int categoryId);

        /// <summary>
        /// Get question by id.
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns>Instance of question.</returns>
        Question GetQuestionById(int questionId);

        /// <summary>
        ///  Get questions by category id.
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns>Collection of questions</returns>
        IEnumerable<Question> GetQuestionsByCategory(string categoryName);

    }
}
