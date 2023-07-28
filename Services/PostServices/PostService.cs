using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.PostModels;

namespace Services.PostServices
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _db; readonly int _userId = 0;

        // public PostService(IHttpContextAccessor httpContext, PostWatchContext db)
        // {
        public PostService(ApplicationDbContext db, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {

            _db = db;
            var user = signInManager.Context.User;
            _userId = int.Parse(userManager.GetUserId(user) ?? "0");
            // var userClaims = httpContext.HttpContext.User.Identity as ClaimsIdentity;
            // var value = userClaims?.FindFirst("Id")?.Value;
            // var validId = int.TryParse(value, out _userId);
            // if (!validId)
            // {
            //     throw new Exception("Attempted to build NoteService without User Id Claim");
            // }
        }
        public async Task<bool> AddPostToTrailAsync(PostCreate req)
        {

            var entity = new Post
            {
                UserId = _userId,
                Title = req.Title,
                TrailId = req.TrailId,
                RegionId = req.RegionId,
                Type = req.Type,
                Content = req.Content,
                Date = DateTime.Now,
                FileName=req.FileName

            };
            _db.Posts.Add(entity);
            
            
            var numberOfChanges = await _db.SaveChangesAsync();
            Console.WriteLine("Number of Changes: "+numberOfChanges);
            var postId = entity.Id;
            if (req.Image != null)
            {

                var filePath = Path.Combine("/Dev/ElevenFiftyProjects/Projects/MVC/RedBadge/TrailWatchMVC/wwwroot/UserUploads/Posts",_userId+"__"+postId+".jpg");
                Console.WriteLine(postId);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await req.Image.CopyToAsync(stream);
                }

                
            }
            
                return numberOfChanges == 1;

        }
        public async Task<bool> AddPostToRegionAsync(PostCreate req)
        {

            var entity = new Post
            {
                UserId = _userId,
                Title = req.Title,
                RegionId = req.RegionId,
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
            var entities = await _db.Posts.Where(t => t.TrailId == id).Select(t => new PostListItem
            {
                Id = t.Id,
                Title = t.Title,
                TrailId = t.TrailId,
                Type = t.Type,
                Date = t.Date


            }).ToListAsync();

            return entities;


        }
        public async Task<IEnumerable<PostListItem?>> GetPostsByRegionIdAsync(int id)
        {
            var entities = await _db.Posts.Where(t => t.RegionId == id).Select(t => new PostListItem
            {
                Id = t.Id,
                Title = t.Title,
                RegionId = t.RegionId,
                Type = t.Type,
                Date = t.Date


            }).ToListAsync();

            return entities;


        }

        public async Task<PostDetail?> GetPostByIdAsync(int id)
        {
            var entity = await _db.Posts.Include(c => c.Comments).ThenInclude(c => c.User).Include(p => p.Region).Include(p => p.Trail).Include(p=>p.User).FirstOrDefaultAsync(r => r.Id == id);
            if (entity is null) return null;
            var note = new PostDetail
            {
                Title = entity.Title,
                Content = entity.Content,
                Id = entity.Id,
                Type = entity.Type,
                Date = entity.Date,
                Comments = entity.Comments,
                User = new Models.UserModels.UserDetail{
                    Id=entity.User.Id,
                    UserName=entity.User.Username,
                }
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