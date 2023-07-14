using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Data.Entities
{
    public class Identity
    {
        public class RoleEntity : IdentityRole<int> { }
        public class UserRoleEntity : IdentityUserRole<int> { }
        public class UserClaimEntity : IdentityUserClaim<int> { }
        public class UserLoginEntity : IdentityUserLogin<int> { }
        public class UserTokenEntity : IdentityUserToken<int> { }
        public class RoleClaimEntity : IdentityRoleClaim<int> { }
    }
}