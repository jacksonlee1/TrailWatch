using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.CommentModels;

namespace Services.CommentServices
{
    public interface ICommentService
    {
        Task<bool> AddCommentAsync(CommentCreate trail);
        Task<IEnumerable<CommentListItem?>> GetAllCommentsAsync();
        Task<CommentDetail?> GetCommentByIdAsync(int id);
        Task<bool> UpdateCommentAsync(CommentUpdate update);
        Task<bool> DeleteCommentByIdAsync(int id);
    }
}