using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM;

namespace BLL.Interface.Services
{
    /// <summary>
    /// The interface provides methods for working with comments.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Create new comment.
        /// </summary>
        /// <param name="comment">Instance of comment.</param>
        void CreateComment(Comment comment);

        /// <summary>
        /// Delete comment.
        /// </summary>
        /// <param name="commentId">Unique identifier of comment.</param>
        void DeleteComment(int commentId);

        /// <summary>
        /// Update comment.
        /// </summary>
        /// <param name="comment">Instance of comment.</param>
        void UpdateComment(Comment comment);

        /// <summary>
        /// Get comments of user.
        /// </summary>
        /// <param name="userId">Unique identifier of user.</param>
        /// <returns></returns>
        IEnumerable<Comment> GetCommentsOfUser(int userId);

        /// <summary>
        /// Get comment by question id.
        /// </summary>
        /// <param name="questionId">Unique identifier of question.</param>
        /// <returns></returns>
        IEnumerable<Comment> GetCommentsByQuestionId(int questionId);


        /// <summary>
        /// Get all comments.
        /// </summary>
        /// <returns>Collection of commnets.</returns>
        IEnumerable<Comment> GetAllComments();

        /// <summary>
        /// Get comment by id.
        /// </summary>
        /// <param name="commentId"></param>
        /// <returns>Instance of comment.</returns>
        Comment GetCommentById(int commentId);

        /// <summary>
        /// Update group comments.
        /// </summary>
        /// <param name="questionId">Unique identifier of question.</param>
        /// <param name="commentId"> The Array of Unique identifier of comments.</param>
        void UpdateGroupComment(int questionId,int[] commentId);
    }
}
