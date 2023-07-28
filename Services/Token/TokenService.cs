using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Data;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Token;

namespace Services.Token
{
    public class TokenService:ITokenService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
       public TokenService(ApplicationDbContext context, IConfiguration configuration)
       {
        _context = context;
        _configuration = configuration;
    }
       public async Task<TokenResponse?> GetTokenAsync(TokenRequest model)
       {
        var userEntity = await GetValidUserAsync(model);
        if(userEntity is null){
            return null;        }
        return GenerateToken(userEntity);
       }

       private async Task<UserEntity?> GetValidUserAsync(TokenRequest model){
            var entity = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == model.UserName.ToLower());
            if (entity is null){
                Console.WriteLine("User not Found");
                return null;
            }
            var passwordHasher = new PasswordHasher<UserEntity>();
            var verifyPasswordResult = passwordHasher.VerifyHashedPassword(entity, entity.Password,model.Password);
            if(verifyPasswordResult == PasswordVerificationResult.Failed){
                Console.WriteLine("Password Incorrect");
                return null;
            }
            return entity;
       }
       private TokenResponse GenerateToken(UserEntity entity){
        var claims = GetClaims(entity);
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration?["Jwt:Key"]??"none"));
        var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration?["Jwt:Issuer"]??"none",
            Audience = _configuration?["Jwt:Audience"]??"none",
            Subject = new ClaimsIdentity(claims),
            IssuedAt = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(14),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenResponse = new TokenResponse
        {
            UserId = entity.Id,
            Token = tokenHandler.WriteToken(token),
            IssuedAt = token.ValidFrom,
            Expires = token.ValidTo
        };
        return tokenResponse;
        
       }

       private Claim[] GetClaims(UserEntity user){
        
        var identifier = _configuration["ClaimTypes:Id"] ?? "Id";
        var claims = new Claim[]
        {
           
            new Claim(identifier,user.Id.ToString()),
            new Claim("UserName", user.UserName),
            new Claim("Email",user.Email)

        };
        return claims;
       }
    }
}