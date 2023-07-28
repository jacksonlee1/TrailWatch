using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Models.UserModels
{
    public class UserRegister
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string ConfirmPassword { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        


        
    }
}