using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Token
{
    public class TokenResponse
    {
        public int UserId{get;set;}
        public string Token{get;set;} = string.Empty;
        public DateTime IssuedAt{get;set;}
        public DateTime Expires{get;set;}
        
    }
}