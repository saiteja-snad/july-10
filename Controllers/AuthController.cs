using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SMS.DTOS.UserDtos;
using StudentManagementSystem.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/v1/auth")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    //===========================================
    [HttpPost("login")]
    [SwaggerOperation( Summary = "User Login", Description = "Authenticate user and generate JWT token")]
    [SwaggerResponse(200, "Login Successful")]
    [SwaggerResponse(401, "Invalid Credentials")]
    public async Task<IActionResult> Login( [FromBody] LoginRequestDTO request)
    {
        var result = await _authService.LoginAsync(request);
        return Ok(result);
    }
}
