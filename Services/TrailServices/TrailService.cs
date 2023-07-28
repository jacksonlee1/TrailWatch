using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models.PostModels;
using Models.TrailModels;
using Models.UserModels;

namespace Services.TrailServices
{
    public class TrailService:ITrailService
    {
         private readonly ApplicationDbContext _db; readonly int _userId;
         
    
        public TrailService(IHttpContextAccessor httpContext, ApplicationDbContext db, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {

            _db = db;
            var user = signInManager.Context.User;
            _userId = int.Parse(userManager.GetUserId(user)??"0");
        //     var userClaims = httpContext.HttpContext.User.Identity as ClaimsIdentity;
        //    var identifierClaimType = config["ClaimTypes:Id"] ?? "Id";
        // var value = userClaims?.FindFirst(identifierClaimType)?.Value;
        //     var validId = int.TryParse(value, out _userId);
        //     if (!validId)
        //     {
        //         throw new Exception("Attempted to build NoteService without User Id Claim");
        //     }
        }
         public async Task<bool> AddTrailAsync(TrailCreate trail)
        {

            var entity = new Trail
            {
                AdminId = _userId,
                RegionId = trail.RegionId,
                Name = trail.Name,
                Type = "state",
                Difficulty = trail.Difficulty,
                Status = 1
            };
            _db.Trails.Add(entity);
            var numberOfChanges = await _db.SaveChangesAsync();
           return numberOfChanges == 1;
           
        }
         public async Task<IEnumerable<TrailListItem?>> GetAllTrailsAsync()
        {
            var entities = await _db.Trails.Select(t => new TrailListItem
            {
               Name = t.Name,
               Status = (TrailStatus)t.Status,
               LastUpdate = t.LastUpdate

               
            }).ToListAsync();

            return entities;


        }

        public async Task<TrailDetail?> GetTrailByIdAsync(int? id)
        {
            var entity = await _db.Trails.Include(r=>r.Posts).ThenInclude(p=>p.User).FirstOrDefaultAsync(r=> r.Id == id);
            if (entity is null) return null;
            var model = new TrailDetail
            {
                AdminId = entity.AdminId??0,
                Id=entity.Id,
                Name = entity.Name,
                RegionId = entity.RegionId,
                Type = entity.Type,
                Status=(TrailStatus)entity.Status,
                Posts = entity.Posts.Select(p=> new PostDetail{
                    Id=p.Id,
                    Title = p.Title,
                    Type=p.Type,
                    Content=p.Content,
                    User = new UserDetail{
                    UserName = p.User.Username,
                    Id=p.User.Id
                    },
                    TrailId=id??0

                }).OrderByDescending(p=>p.Date).ToList()
            };
            return model;
                   }

         public async Task<IEnumerable<TrailListItem?>> GetTrailsByRegionIdAsync(int id)
        {
            var entities = await _db.Trails.Include(t=> t.Region).Where(t=>t.Id ==id).Select(t => new TrailListItem
            {
               Id = t.Id, 
               Name = t.Region.Name,
               Status = (TrailStatus)t.Status,
               LastUpdate = t.LastUpdate,



               
            }).ToListAsync();



            return entities;


        }

        public async Task<bool> UpdateTrailAsync(TrailUpdate update)
        {
            var entity = await _db.Trails.FindAsync(update.Id);
            if (entity==null) return false;
            // entity.Name = update.Name;
            // entity.AdminId = update.AdminId;
            // entity.RegionId = update.RegionId;
            // entity.Type = update.Type;
            // entity.Difficulty = update.Difficulty;
            entity.Status = (int)update.Status;
            entity.LastUpdate = DateTime.Now;
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1; 
        }
        public async Task<bool> DeleteTrailByIdAsync(int id)
        {
            var entity = await _db.Trails.FindAsync(id);

            if (entity==null ||entity.AdminId != _userId) return false;
            _db.Trails.Remove(entity);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;

        }

    }
}