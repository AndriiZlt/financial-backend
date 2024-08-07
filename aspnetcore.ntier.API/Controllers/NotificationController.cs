﻿
using aspnetcore.ntier.BLL;
using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace aspnetcore.ntier.API.Controllers
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]

    public class NotificationController : ControllerBase
    {

        private readonly INotificationService _notificationService;


        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpGet("getnotifications")]
        public async Task<IActionResult> GetNotifications()
        {
            try
            {
                var result = await _notificationService.GetNotificationsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Error occurred in GetNotifications controller: {ex}", ex.Message);
                return BadRequest($"Error occurred in GetNotifications controller: {ex.Message}");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpPut("readnotification/{notificationId}")]
        public async Task<IActionResult> ReadNotification(string notificationId)
        {
            try
            {
                
                return Ok(await _notificationService.ReadNotificationAsync(notificationId));
            }
            catch (Exception ex)
            {
                Log.Error("Error in ReadNotification controller: {ex}", ex.Message);
                return BadRequest($"Error occurred in AddNotification controller: {ex.Message}");
            }
        }

    }
}
