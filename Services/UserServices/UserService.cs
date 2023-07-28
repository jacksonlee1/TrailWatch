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
    
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    public UserService(
        ApplicationDbContext context,
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager)
    {
        _db = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> LoginAsync(UserLogin model)
    {
        // verifies the user exists by the username
        var userEntity = await _userManager.FindByNameAsync(model.Username);
        if (userEntity is null)
            return false;
        // verifies the correct password was given
        var passwordHasher = new PasswordHasher<UserEntity>();
        var verifyPasswordResult = passwordHasher.VerifyHashedPassword(userEntity, userEntity.Password, model.Password);
        if (verifyPasswordResult == PasswordVerificationResult.Failed)
            return false;
        // finally, since user exists and password passes verification, sign in the user
        await _signInManager.SignInAsync(userEntity, true);
        return true;
    }
    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
       public async Task<bool> RegisterUserAsync(UserRegister model)
    {
        if (await GetUserByEmailAsync(model.Email) != null)
            return false;
        UserEntity entity = new()
        {
            Email = model.Email,
            Username = model.Username,
        };
        var passwordHasher = new PasswordHasher<UserEntity>();
        entity.Password = passwordHasher.HashPassword(entity, model.Password);
        var createResult = await _userManager.CreateAsync(entity);
        return createResult.Succeeded; //replaces the following
        // _context.Users.Add(entity);
        // int numberOfChanges = await _context.SaveChangesAsync();
        // return numberOfChanges == 1;
    }
    public async Task<bool> UpdateUserByIdAsync(UserUpdate request)
    {
        var userEntity = await _db.Users.FindAsync(request.Id);
        if (userEntity is null)
            return false;
        // update entity's properties
        userEntity.Email = request.Email;
        userEntity.Password = request.Password;
        if (request.Image != null)
            {

                var filePath = Path.Combine("/Dev/ElevenFiftyProjects/Projects/MVC/RedBadge/TrailWatchMVC/wwwroot/UserUploads/ProfilePics",_userId+".jpg");
                Console.WriteLine(request.Image.FileName);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await request.Image.CopyToAsync(stream);
                }

                
            }
        var createResult = await _userManager.CreateAsync(userEntity);
        return createResult.Succeeded;
    }
    public async Task<bool> DeleteUserAsync(int id)
    {
        var userEntity = await _db.Users.FindAsync(id);
        // remove the ingredient from the dbdb and assert that one change was saved
        _db.Users.Remove(userEntity);
        return false;
    }
    private async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        return await _db.Users.FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
    }
    // private async Task<User?> GetUserByUsernameAsync(string username)
    // {
    //     return await _db.Users.FirstOrDefaultAsync(user => user.UserName.ToLower() == username.ToLower());
    // }
    // private async Task<bool> UserExistsAsync(string email, string username)
    // {
    //     var normalizedEmail = _userManager.NormalizeEmail(email);
    //     var NormalizedUsername = _userManager.NormalizeName(username);
    //     return await _db.Users.AnyAsync(y =>
    //     y.NormalizedEmail == normalizedEmail ||
    //     y.NormalizedUserName == NormalizedUsername
    //     );
    // }

     public async Task<UserEntity?> GetUserByIdAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
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





























//         public async Task<bool> RegisterUserAsync(UserRegister model)
//         {
//             if (GetUserByUserNameAsync(model.Username) is null) return false;
//             var entity = new User
//             {
//                 Username = model.Username,
//                 UserEmail = model.Email,
//             };
//             var passwordHasher = new PasswordHasher<User>();
//             entity.Password = passwordHasher.HashPassword(entity, model.Password);
//             _db.Users.Add(entity);
//             var numChanges = await _db.SaveChangesAsync();
//             return numChanges == 1;
//         }
//         public async Task<User?> GetUserByUserNameAsync(string username)
//         {
//             return await _db.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == username.ToLower());
//         }
//        
//         public async Task<bool> UpdateUserAsync(UserUpdate req)
//         {
//             var entity = await _db.Users.FindAsync(req.Id);
//             if (entity is null) return false;
//             if (entity.Id != _userId) return false;
//             entity.UserName = req.UserName;
            
//             entity.Email = req.Email;
         

//             var passwordHasher = new PasswordHasher<User>();
//             entity.Password = passwordHasher.HashPassword(entity, req.Password);
//             var numChanges = await _db.SaveChangesAsync();
//             return numChanges == 1;
//         }
//         public async Task<bool> UpdateCurrentUserAsync(UserUpdate req)
//         {
//             if (_userId == 0) return false;
//             var entity = await _db.Users.FindAsync(_userId);
//             if (entity is null) return false;
//             if (entity.Id != _userId) return false;
//               entity.UserName = req.UserName;
            
//             entity.Email = req.Email;
//             var passwordHasher = new PasswordHasher<User>();
//             entity.Password = passwordHasher.HashPassword(entity, req.Password);
//             var numChanges = await _db.SaveChangesAsync();
//             return numChanges == 1;
//         }
//         public async Task<bool> DeleteUserByIdAsync(int id)
//         {
//             var entity = await _db.Users.FindAsync(id);
//             if (entity is null) return false;
//             _db.Users.Remove(entity);
//             var numChanges = await _db.SaveChangesAsync();
//             return numChanges == 1;
//         }
//         public async Task<bool> DeleteUserByCurrentUserAsync()
//         {
//             if (_userId == 0) return false;
//             var entity = await _db.Users.FindAsync(_userId);
//             if (entity is null) return false;
//             _db.Users.Remove(entity);
//             var numChanges = await _db.SaveChangesAsync();
//             return numChanges == 1;
//         }
//         public async Task<User?> GetUserByCurrentUserAsync()
//         {
//             if (_userId == 0) return null;
//             var entity = await _db.Users.FindAsync(_userId);
//             if (entity is null) return null;
//             return entity;
//         }
//        
//     }
// }