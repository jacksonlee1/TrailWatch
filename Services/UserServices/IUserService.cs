using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Models.UserModels;

namespace Services.UserServices
{
    public interface IUserService
    {
   
        Task<bool> RegisterUserAsync(UserRegister model);
        Task<UserEntity?> GetUserByIdAsync(int id);
        Task<List<UserDetail>> GetAllUsersAsync();
        Task<bool> DeleteUserAsync(int id);
        Task<bool> UpdateUserByIdAsync(UserUpdate req);

        Task<bool> LoginAsync(UserLogin model);
        Task LogoutAsync();
    
      
    }
}