using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.BLL.Utilities.CustomExceptions;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace aspnetcore.ntier.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class FriendController : ControllerBase
{
    private readonly IFriendService _friendService;

    public FriendController(IFriendService friendService)
    {
        _friendService = friendService;
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("getfriends")]
    public async Task<IActionResult> GetFriends()
    {
        try
        {
            return Ok(await _friendService.GetFriendsAsync());
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetFriends controller", ex);
            return BadRequest("Something went wrong");
        }
    }


    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("addfriend")]
    public async Task<IActionResult> AddFriend(FriendToAddDTO friendToAddDTO)
    {
        try
        {
            return Ok(await _friendService.AddFriendAsync(friendToAddDTO));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in AddFriend controller", ex);
            return BadRequest("Something went wrong");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpDelete("deletefriend")]
    public async Task<IActionResult> DeleteFriend(int friendId)
    {
        try
        {
            await _friendService.DeleteFriendAsync(friendId);
            return Ok();
        }
        catch (UserNotFoundException ex)
        {
            Log.Error("UserNotFound Exception in DeleteFriend controller", ex);
            return NotFound("Friend not found");
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in DeleteFriend controller", ex);
            return BadRequest("Something went wrong");
        }
    }
}
