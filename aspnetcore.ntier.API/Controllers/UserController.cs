using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.BLL.Utilities.CustomExceptions;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace aspnetcore.ntier.API.Controllers;
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("getusers")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            return Ok(await _userService.GetUsersAsync());
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetUsers controller", ex);
            return BadRequest("Something went wrong");
        }
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("getuser")]
    public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
    {
        try
        {
            return Ok(await _userService.GetUserAsync(cancellationToken));
        }
        catch (UserNotFoundException ex)
        {
            Log.Error("UserNotFoundException in GetUser controller", ex);
            return NotFound("User not found");
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetUser controller", ex);
            return BadRequest("Something went wrong");
        }
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("adduser")]
    public async Task<IActionResult> AddUser(UserToAddDTO userToAddDTO)
    {
        try
        {
            return Ok(await _userService.AddUserAsync(userToAddDTO));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in AddUser controller", ex);
            return BadRequest("Something went wrong");
        }
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPut("updateuser")]
    public async Task<IActionResult> UpdateUser(UserToUpdateDTO userToUpdateDTO)
    {
        try
        {
            return Ok(await _userService.UpdateUserAsync(userToUpdateDTO));
        }
        catch (UserNotFoundException ex)
        {
            Log.Error("UserNotFoundException in UpdateUser controller", ex);
            return NotFound("User not found");
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in UpdateUser controller", ex);
            return BadRequest("Something went wrong");
        }
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpDelete("deleteuser")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        try
        {
            await _userService.DeleteUserAsync(userId);
            return Ok();
        }
        catch (UserNotFoundException ex)
        {
            Log.Error("UserNotFoundException in DeleteUser controller", ex);
            return NotFound("User not found");
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in DeleteUser controller", ex);
            return BadRequest("Something went wrong");
        }
    }
}
