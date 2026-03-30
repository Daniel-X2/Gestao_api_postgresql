using System.IdentityModel.Tokens.Jwt;
using System.Text;
using auth.Models;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Dto;

namespace auth.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;
   public  TokenService(IConfiguration config)
   {
       _configuration = config;
   }
    public string Generate(Users user,bool isadmin)
    {
        var handler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.KMUFsIDTnFmyG3nMiGM6H9FNFUROf3wh7SmqJp-QV30");//aqui vai o a chave
       var credetial= new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature);

       var tokenDescriptor = new SecurityTokenDescriptor
       {
           Subject = GenerateClaims(user,isadmin ),
           SigningCredentials = credetial,
           Expires = DateTime.UtcNow.AddHours(2),
           Issuer = _configuration["jwt:Issuer"],
                        Audience = _configuration["jwt:Audience"]

       };
       var token = handler.CreateToken(tokenDescriptor);

        var strToken = handler.WriteToken(token);
        return strToken;
    }

    private static ClaimsIdentity GenerateClaims(Users users,bool isadmin)
    {
        var ci = new ClaimsIdentity();
        ci.AddClaim(new Claim(ClaimTypes.Name,users.cpf));
        if (isadmin)
        {
            ci.AddClaim(new Claim(ClaimTypes.Role,"Admin"));
            return ci;
        }
        ci.AddClaim(new Claim(ClaimTypes.Role,"User"));
        return ci;
    }
}