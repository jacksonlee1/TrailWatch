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
        Task<User?> GetUserByIdAsync(int id);
        Task<List<UserDetail>> GetAllUsersAsync();
        Task<bool> DeleteUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(UserUpdate req);
        Task<bool> UpdateCurrentUserAsync(UserUpdate req);
        Task<User?> GetUserByCurrentUserAsync();
      
    }
}