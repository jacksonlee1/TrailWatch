using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.PostModels;

namespace Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _db; readonly int _userId;

        // public PostService(IHttpContextAccessor httpContext, PostWatchContext db)
        // {
        public PostService(ApplicationDbContext db)
        {
            _db = db;
            // var userClaims = httpContext.HttpContext.User.Identity as ClaimsIdentity;
            // var value = userClaims?.FindFirst("Id")?.Value;
            // var validId = int.TryParse(value, out _userId);
            // if (!validId)
            // {
            //     throw new Exception("Attempted to build NoteService without User Id Claim");
            // }
        }
        public async Task<bool> AddPostAsync(PostCreate req)
        {

            var entity = new Post
            {

                Title = req.Title,
                Type = req.Type,
                Content = req.Content,
                Date = DateTime.Now

            };
            _db.Posts.Add(entity);
            var numberOfChanges = await _db.SaveChangesAsync();
            return numberOfChanges == 1;

        }
        public async Task<IEnumerable<PostListItem?>> GetAllPostsAsync()
        {
            var entities = await _db.Posts.Select(t => new PostListItem
            {
                Title = t.Title,
                Type = t.Type,
                Date = t.Date


            }).ToListAsync();

            return entities;


        }
         public async Task<IEnumerable<PostListItem?>> GetPostsByTrailIdAsync(int id)
        {
            var entities = await _db.Posts.Where(t=>t.TrailId == id).Select(t => new PostListItem
            {
                Title = t.Title,
                Type = t.Type,
                Date = t.Date


            }).ToListAsync();

            return entities;


        }

        public async Task<PostDetail?> GetPostByIdAsync(int id)
        {
            var entity = await _db.Posts.FirstOrDefaultAsync(r => r.Id == id);
            if (entity is null) return null;
            var note = new PostDetail
            {
                Title = entity.Title,
                Content = entity.Content,
                Id = entity.Id,
                Type = entity.Type,
                Date = entity.Date
            };
            return note;
        }

        public async Task<bool> UpdatePostAsync(PostUpdate update)
        {
            var entity = await _db.Posts.FindAsync(update.Id);
            if (entity == null) return false;
            //if (entity==null || entity.AdminId != _userId) return false;
            entity.Title = update.Title;
            entity.Content = update.Content;

            entity.Type = update.Type;

            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }
        public async Task<bool> DeletePostByIdAsync(int id)
        {
            var entity = await _db.Posts.FindAsync(id);
            if (entity == null) return false;
            //if (entity==null || entity.AdminId != _userId) return false;
            _db.Posts.Remove(entity);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;

        }

    }
}