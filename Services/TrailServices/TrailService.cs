using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.TrailModels;

namespace Services.TrailServices
{
    public class TrailService:ITrailService
    {
         private readonly ApplicationDbContext _db; readonly int _userId;
    
        // public TrailService(IHttpContextAccessor httpContext, ApplicationDbContext db)
        // {
             public TrailService(ApplicationDbContext db)
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
         public async Task<bool> AddTrailAsync(TrailCreate trail)
        {

            var entity = new Trail
            {
                AdminId = _userId,
                RegionId = trail.RegionId,
                Name = trail.Name,
                Type = trail.Type,
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
               Status = t.Status,
               LastUpdate = t.LastUpdate

               
            }).ToListAsync();

            return entities;


        }

        public async Task<TrailDetail?> GetTrailByIdAsync(int id)
        {
            var entity = await _db.Trails.FindAsync(id);
            if (entity is null) return null;
            var note = new TrailDetail
            {
                AdminId = id,
                Name = entity.Name,
                RegionId = entity.RegionId,
                Type = entity.Type,
                Difficulty = entity.Difficulty,
                Status = entity.Status,
                LastUpdate = entity.LastUpdate
            };
            return note;
        }

         public async Task<IEnumerable<TrailListItem?>> GetTrailsByRegionIdAsync(int id)
        {
            var entities = await _db.Trails.Where(t=>t.RegionId == id).Select(t => new TrailListItem
            {
               Name = t.Name,
               Status = t.Status,
               LastUpdate = t.LastUpdate

               
            }).ToListAsync();

            return entities;


        }

        public async Task<bool> UpdateTrailAsync(TrailUpdate update)
        {
            var entity = await _db.Trails.FindAsync(update.Id);
            if (entity==null || entity.AdminId != _userId) return false;
            entity.Name = update.Name;
            entity.AdminId = update.AdminId;
            entity.RegionId = update.RegionId;
            entity.Type = update.Type;
            entity.Difficulty = update.Difficulty;
            entity.Status = update.Status;
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