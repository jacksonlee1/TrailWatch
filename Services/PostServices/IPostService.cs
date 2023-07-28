using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.PostModels;

namespace Services.PostServices
{
    public interface IPostService
    {
      Task<bool> AddPostToRegionAsync(PostCreate req);
        Task<IEnumerable<PostListItem?>> GetAllPostsAsync();
        Task<IEnumerable<PostListItem?>> GetPostsByTrailIdAsync(int id);
        Task<PostDetail?> GetPostByIdAsync(int id);
        Task<bool> UpdatePostAsync(PostUpdate update);
        Task<bool> DeletePostByIdAsync(int id);
        Task<IEnumerable<PostListItem?>> GetPostsByRegionIdAsync(int id);
        Task<bool> AddPostToTrailAsync(PostCreate req);
    }
}