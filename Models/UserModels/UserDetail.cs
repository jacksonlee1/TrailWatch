using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.UserModels
{
    public class UserDetail
    {
     
        public int Id{get;set;}
        public string UserName { get; set; } = string.Empty;
     
        public string Email { get; set; } = string.Empty;
    }
}