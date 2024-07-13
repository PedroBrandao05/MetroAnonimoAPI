using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AnonymityService.Middleware.Security.Contracts;
using AnonymityService.Middleware.Security.Errors;
using AnonymityService.Middleware.Security.Models;
using Microsoft.IdentityModel.Tokens;

namespace AnonymityService.Middleware.Security;

public class Jwt : IJwt
{
  private string Secret { get; set; }

  private readonly SymmetricSecurityKey _signingKey;

  public Jwt(string secret)
  {
    Secret = secret;
    
    _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret));
  }
  
  public string Generate(string userCode)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenDescriptor = new SecurityTokenDescriptor
    {
      Subject = new ClaimsIdentity(new[] { new Claim("userCode", userCode) }),
      Expires = DateTime.UtcNow.AddHours(1),
      SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    return tokenHandler.WriteToken(token);
  }

  public SessionJwtPayload? Decode(string token)
  {
    var tokenHandler = new JwtSecurityTokenHandler();
    try
    {
      var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = _signingKey,
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false
      }, out SecurityToken validatedToken);

      var userCodeClaim = principal.FindFirst("userCode");
      if (userCodeClaim == null)
      {
        throw new InvalidToken("Token is missing user code");
      }

      return new SessionJwtPayload
      {
        UserCode = userCodeClaim.Value
      };
    }
    catch
    {
      return null;
    }
  }
}