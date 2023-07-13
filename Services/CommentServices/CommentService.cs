using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.CommentModels;

namespace Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _db; readonly int _userId;

        // public CommentService(IHttpContextAccessor httpContext, CommentWatchContext db)
        // {
        public CommentService(ApplicationDbContext db)
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
        public async Task<bool> AddCommentAsync(CommentCreate req)
        {

            var entity = new Comment
            {

                PostId = req.PostId,
                UserId = req.UserId,
                Content = req.Content,


            };
            _db.Comments.Add(entity);
            var numberOfChanges = await _db.SaveChangesAsync();
            return numberOfChanges == 1;

        }
        public async Task<IEnumerable<CommentListItem?>> GetAllCommentsAsync()
        {
            var entities = await _db.Comments.Select(t => new CommentListItem
            {
                Id = t.Id,
                UserId = t.UserId,
                Content = t.Content


            }).ToListAsync();

            return entities;


        }

        public async Task<CommentDetail?> GetCommentByIdAsync(int id)
        {
            var entity = await _db.Comments.FirstOrDefaultAsync(r => r.Id == id);
            if (entity is null) return null;
            var note = new CommentDetail
            {

                Content = entity.Content,
                Id = entity.Id,
                UserId = entity.UserId,

            };
            return note;
        }

        public async Task<bool> UpdateCommentAsync(CommentUpdate update)
        {
            var entity = await _db.Comments.FindAsync(update.Id);
            if (entity == null) return false;
            //if (entity==null || entity.AdminId != _userId) return false;
            entity.PostId = update.PostId;
            entity.Content = update.Content;

            entity.UserId = update.UserId;

            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }
        public async Task<bool> DeleteCommentByIdAsync(int id)
        {
            var entity = await _db.Comments.FindAsync(id);
            if (entity == null) return false;
            //if (entity==null || entity.AdminId != _userId) return false;
            _db.Comments.Remove(entity);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;

        }

    }

}