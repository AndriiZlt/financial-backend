using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.BLL.Utilities.CustomExceptions;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace aspnetcore.ntier.API.Controllers.V2;


[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("2")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("getuser")]
    public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await _userService.GetUserAsync(cancellationToken));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An unexpected error occurred in GetUser controller");
            return BadRequest("Something went wrong");
        }
    }

    [HttpGet("getusers")]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await _userService.GetUsersAsync(cancellationToken));
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An unexpected error occurred in GetUsers controller");
            return BadRequest("Something went wrong");
        }
    }
}
