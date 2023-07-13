using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.RegionModels;

namespace Services.RegionServices
{
    public class RegionService:IRegionService
    {
         private readonly ApplicationDbContext _db; readonly int _userId;
    
        // public RegionService(IHttpContextAccessor httpContext, RegionWatchContext db)
        // {
             public RegionService(ApplicationDbContext db)
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
         public async Task<bool> AddRegionAsync(RegionCreate req)
        {

            var entity = new Region
            {
                AdminId = _userId,
                Name = req.Name,
                Type = req.Type,
          
            };
            _db.Regions.Add(entity);
            var numberOfChanges = await _db.SaveChangesAsync();
           return numberOfChanges == 1;
           
        }
         public async Task<IEnumerable<RegionListItem?>> GetAllRegionsAsync()
        {
            var entities = await _db.Regions.Select(t => new RegionListItem
            {
               Name = t.Name,
               Type = t.Type

               
            }).ToListAsync();

            return entities;


        }

        public async Task<RegionDetail?> GetRegionByIdAsync(int id)
        {
            var entity = await _db.Regions.Include(r=>r.Admin).FirstOrDefaultAsync(r=> r.Id == id);
            if (entity is null) return null;
            var note = new RegionDetail
            {
                Admin = entity.Admin,
                Name = entity.Name,
                RegionId = entity.Id,
                Type = entity.Type
            };
            return note;
        }

        public async Task<bool> UpdateRegionAsync(RegionUpdate update)
        {
            var entity = await _db.Regions.FindAsync(update.Id);
            if (entity==null || entity.AdminId != _userId) return false;
            entity.Name = update.Name;
            entity.AdminId = update.AdminId;
         
            entity.Type = update.Type;
           
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1; 
        }
        public async Task<bool> DeleteRegionByIdAsync(int id)
        {
            var entity = await _db.Regions.FindAsync(id);

            if (entity==null ||entity.AdminId != _userId) return false;
            _db.Regions.Remove(entity);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;

        }

    }
}