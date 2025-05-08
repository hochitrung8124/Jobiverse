using System;
using API.Sevices;

namespace API.Middlewares;

public class JwtMiddleware
{
  private readonly RequestDelegate _next;
  private readonly JwtService _jwtService;
  private readonly ILogger<JwtMiddleware> _logger;

  public JwtMiddleware(RequestDelegate next, JwtService jwtService, ILogger<JwtMiddleware> logger)
  {
    _next = next;
    _jwtService = jwtService;
    _logger = logger;
  }

  public async Task InvokeAsync(HttpContext context)
  {
    var token = context.Request.Cookies["jwt"];
    if (token != null)
    {
      var principal = _jwtService.VerifyToken(token);
      if (principal != null)
      {
        context.User = principal;
      }
      else
      {
        _logger.LogWarning("Invalid or expired JWT token.");
      }
    }
    await _next(context);
  }
}
