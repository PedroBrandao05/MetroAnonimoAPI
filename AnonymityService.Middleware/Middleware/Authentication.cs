using AnonymityService.Middleware.Security.Contracts;
using Microsoft.AspNetCore.Http;

namespace AnonymityService.Middleware.Middleware;

public class Authentication
{
  private readonly RequestDelegate _next;

  private readonly IJwt _jwt;
  
  public Authentication(RequestDelegate next)
  {
    _next = next;
  }
  
  public async Task InvokeAsync(HttpContext context)
  {
    if (!context.Request.Headers.ContainsKey("Authorization"))
    {
      context.Response.StatusCode = StatusCodes.Status401Unauthorized;
      await context.Response.WriteAsync("Authorization header is missing.");
      return;
    }

    var token = context.Request.Headers["Authorization"].ToString();
    if (!token.StartsWith("Bearer "))
    {
      context.Response.StatusCode = StatusCodes.Status401Unauthorized;
      await context.Response.WriteAsync("Invalid token format.");
      return;
    }

    token = token.Substring("Bearer ".Length).Trim();

    var payload = _jwt.Decode(token);
    if (payload == null)
    {
      context.Response.StatusCode = StatusCodes.Status401Unauthorized;
      await context.Response.WriteAsync("Invalid token.");
      return;
    }

    context.Items["SessionJwtPayload"] = payload;

    await _next(context);
  }
}