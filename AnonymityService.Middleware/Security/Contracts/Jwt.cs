using AnonymityService.Middleware.Security.Models;

namespace AnonymityService.Middleware.Security.Contracts;

public interface IJwt
{
  public string Generate(string userCode);

  public SessionJwtPayload? Decode(string token);
}