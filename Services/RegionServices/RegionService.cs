using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.PostModels;
using Models.RegionModels;
using Models.UserModels;

namespace Services.RegionServices
{
    public class RegionService:IRegionService
    {
         private readonly ApplicationDbContext _db; readonly int _userId = 0;
         private IHttpContextAccessor _httpContextAccessor;

              private readonly UserManager<UserEntity> _userManager;
        private readonly SignInManager<UserEntity> _signInManager;
    
        // public RegionService(IHttpContextAccessor httpContext, RegionWatchContext db)
        // {
             public RegionService(ApplicationDbContext db, SignInManager<UserEntity> signInManager, UserManager<UserEntity> userManager)
        {

            _db = db;
            var user = signInManager.Context.User;
            _userId = int.Parse(userManager.GetUserId(user));       // var userClaims = httpContext.HttpContext.User.Identity as ClaimsIdentity;
            // var value = userClaims?.FindFirst("Id")?.Value;
            // var validId = int.TryParse(value, out _userId);
            // if (!validId)
            // {
            //     throw new Exception("Attempted to build NoteService without User Id Claim");
            // }
        }
         public async Task<bool> AddRegionAsync(RegionCreate req)
        {
            Console.WriteLine(_userId);
            if(_userId!=0){
                
            
            var entity = new Region
            {
                AdminId = _userId,
                Name = req.Name,
                Type = "state",
          
            };
        Console.WriteLine(entity.AdminId);

            _db.Regions.Add(entity);
            var numberOfChanges = await _db.SaveChangesAsync();
           return numberOfChanges == 1;
            }
            return false;
           
        }
         public async Task<IEnumerable<RegionListItem?>> GetAllRegionsAsync()
        {
            var entities = await _db.Regions.Include(r=>r.Trails).Select(t => new RegionListItem
            {
                Id = t.Id,
               Name = t.Name,
               Type ="t.Type",
               NumTrails= t.Trails.Count()

               
            }).ToListAsync();


            return entities;


        }
        public async Task<bool> SubscribeAsync(){
            throw new NotImplementedException();
        }
        public async Task<RegionDetail?> GetRegionByIdAsync(int id)
        {
            var entity = await _db.Regions.Include(r=>r.Trails).Include(r=>r.Posts).ThenInclude(p=>p.User).FirstOrDefaultAsync(r=> r.Id == id);
            if (entity is null) return null;
            var model = new RegionDetail
            {
                AdminId = entity.AdminId??0,
                Trails=  entity.Trails.Select(t=> new Models.TrailModels.TrailDetail{
                    AdminId = t.AdminId,
                    Id=t.Id,
                    RegionId = t.RegionId,
                    Name = t.Name,
                    Type = t.Type,
                    Status = (TrailStatus)t.Status,
                    LastUpdate = t.LastUpdate,
                }).ToList(),
                Name = entity.Name,
                RegionId = entity.Id,
                Type = entity.Type,
                Posts = entity.Posts.Select(p=> new PostDetail{
                    Id=p.Id,
                    Title = p.Title,
                    Type=p.Type,
                    Content=p.Content,
                    User = new UserDetail{
                    UserName = p.User.Username,
                    Id=p.User.Id
                    },
                    RegionId=id

                }).OrderByDescending(p=>p.Date).ToList()
            };
            return model;
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