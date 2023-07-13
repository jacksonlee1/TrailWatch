using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.PostModels;

namespace Services.PostServices
{
    public interface IPostService
    {
      Task<bool> AddPostAsync(PostCreate trail);
        Task<IEnumerable<PostListItem?>> GetAllPostsAsync();
        Task<PostDetail?> GetPostByIdAsync(int id);
        Task<bool> UpdatePostAsync(PostUpdate update);
        Task<bool> DeletePostByIdAsync(int id);
    }
}