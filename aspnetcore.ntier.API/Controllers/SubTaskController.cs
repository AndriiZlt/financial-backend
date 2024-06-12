
using aspnetcore.ntier.BLL.Services;
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

    public class SubtaskController : ControllerBase
    {

        private readonly ISubtaskService _subtaskService;

        public SubtaskController(ISubtaskService subtaskService)
        {
            _subtaskService = subtaskService;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpGet("getsubtasks")]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                var result = await _subtaskService.GetSubtasksAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in GetTasks controller", ex);
                return BadRequest("Something went wrong");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpPost("addsubtask")]
        public async Task<IActionResult> AddTask(SubtaskToAddDTO taskToAddDTO)
        {
            try
            {
                return Ok(await _subtaskService.AddSubtaskAsync(taskToAddDTO));
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in AddTask controller", ex);
                return BadRequest("Something went wrong");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpPut("updatestatus")]
        public async Task<IActionResult> UpdateStatusTask(int taskId)
        {
            try
            {
                return Ok(await _subtaskService.UpdateStatusSubtaskAsync(taskId));
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error("KeyNotFoundException in UpdateStatusTask controller", ex);
                return NotFound("Task not found");
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in UpdateStatusTask controller", ex);
                return BadRequest("Something went wrong");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpPut("updatesubtask")]
        public async Task<IActionResult> UpdateTask(SubtaskDTO taskToUpdate)
        {
            try
            {
                return Ok(await _subtaskService.UpdateSubtaskAsync(taskToUpdate));
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error("KeyNotFoundException in UpdateTask controller", ex);
                return NotFound("Task not found");
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in UpdateTask controller", ex);
                return BadRequest("Something went wrong");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpDelete("deletesubtask")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            try
            {
                await _subtaskService.DeleteSubtaskAsync(taskId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error("KeyNotFoundException in DeleteSubtaskAsync controller", ex);
                return NotFound("Task not found");
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in DeleteSubtaskAsync controller", ex);
                return BadRequest("Something went wrong");
            }
        }

    }
}
