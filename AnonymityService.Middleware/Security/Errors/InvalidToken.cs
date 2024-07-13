using Common.Errors;

namespace AnonymityService.Middleware.Security.Errors;

public class InvalidToken : ApplicationError
{
  public InvalidToken(string? message) : base(403, "INVALID_TOKEN", message)
  {
  }
}