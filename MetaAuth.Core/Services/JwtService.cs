using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MetaAuth.SharedEntities;
using MetaAuth.SharedEntities.AzureCosmosDb;
using Microsoft.IdentityModel.Tokens;

namespace MetaAuth.Core.Services;

public class JwtService : IJwtService
{
    public string GenerateToken(Dictionary<string, string> userData, RegisteredWebAppsModel registeredWebAppsModel)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(registeredWebAppsModel.AppSecret);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        foreach (var data in userData)
        {
            tokenDescriptor.Subject.AddClaim(new Claim(data.Key, data.Value));
        }

        tokenDescriptor.Expires = registeredWebAppsModel.TokenExpireTimeUnit switch
        {
            TokenExpireTimeUnits.Minutes => DateTime.UtcNow.AddMinutes(registeredWebAppsModel.TokenExpire),
            TokenExpireTimeUnits.Hours => DateTime.UtcNow.AddHours(registeredWebAppsModel.TokenExpire),
            TokenExpireTimeUnits.Days => DateTime.UtcNow.AddDays(registeredWebAppsModel.TokenExpire),
            TokenExpireTimeUnits.Months => DateTime.UtcNow.AddMonths(registeredWebAppsModel.TokenExpire),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}