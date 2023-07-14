using System.Security.Claims;
using Data;
using Microsoft.EntityFrameworkCore;
using Models.UserModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly int _userId;
        public UserService(IHttpContextAccessor httpContext, ApplicationDbContext db)
        {
            _db = db;
            var userClaims = httpContext.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims?.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _userId);
            //UserIds start at 1, Cannot be null
            if (!validId) _userId = 0;
        }
        public async Task<bool> RegisterUserAsync(UserRegister model)
        {
            if (GetUserByUserNameAsync(model.UserName) is null) return false;
            var entity = new User
            {
                Username = model.UserName,
                UserEmail = model.Email,
            };
            var passwordHasher = new PasswordHasher<User>();
            entity.Password = passwordHasher.HashPassword(entity, model.Password);
            _db.Users.Add(entity);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }
        public async Task<User?> GetUserByUserNameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<bool> UpdateUserAsync(UserUpdate req)
        {
            var entity = await _db.Users.FindAsync(req.Id);
            if (entity is null) return false;
            if (entity.Id != _userId) return false;
            entity.UserName = req.UserName;
            
            entity.Email = req.Email;
         

            var passwordHasher = new PasswordHasher<User>();
            entity.Password = passwordHasher.HashPassword(entity, req.Password);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }
        public async Task<bool> UpdateCurrentUserAsync(UserUpdate req)
        {
            if (_userId == 0) return false;
            var entity = await _db.Users.FindAsync(_userId);
            if (entity is null) return false;
            if (entity.Id != _userId) return false;
              entity.UserName = req.UserName;
            
            entity.Email = req.Email;
            var passwordHasher = new PasswordHasher<User>();
            entity.Password = passwordHasher.HashPassword(entity, req.Password);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }
        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            var entity = await _db.Users.FindAsync(id);
            if (entity is null) return false;
            _db.Users.Remove(entity);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }
        public async Task<bool> DeleteUserByCurrentUserAsync()
        {
            if (_userId == 0) return false;
            var entity = await _db.Users.FindAsync(_userId);
            if (entity is null) return false;
            _db.Users.Remove(entity);
            var numChanges = await _db.SaveChangesAsync();
            return numChanges == 1;
        }
        public async Task<User?> GetUserByCurrentUserAsync()
        {
            if (_userId == 0) return null;
            var entity = await _db.Users.FindAsync(_userId);
            if (entity is null) return null;
            return entity;
        }
        public async Task<List<UserDetail>> GetAllUsersAsync()
        {
            return await _db.Users.Select(u => new UserDetail
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            }).ToListAsync();
        }
    }
}