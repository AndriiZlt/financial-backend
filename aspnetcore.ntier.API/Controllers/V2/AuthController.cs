using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.BLL.Utilities.CustomExceptions;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace aspnetcore.ntier.API.Controllers.V2;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserToLoginDTO userToLoginDTO)
    {
        try
        {
            var user = await _authService.LoginAsync(userToLoginDTO);

            return Ok(user);
        }
        catch (UserNotFoundException ex)
        {
            Log.Error("UserNotFoundException in Login controller", ex);
            return Unauthorized();
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An unexpected error occurred in Login controller");
            return BadRequest("Something went wrong");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserToRegisterDTO userToRegisterDTO)
    {
        try
        {
            return Ok(await _authService.RegisterAsync(userToRegisterDTO));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in Register controller", ex);
            return BadRequest("Something went wrong");
        }
    }
}
