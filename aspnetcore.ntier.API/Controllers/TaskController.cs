
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
    public class TaskController : ControllerBase
    {

        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpGet("gettasks")]
        public async Task<IActionResult> GetTasks()
        {
            try
            {
                var result = await _taskService.GetTasksAsync();
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
        [HttpPost("addtask")]
        public async Task<IActionResult> AddTask(TaskToAddDTO taskToAddDTO)
        {
            try
            {
                return Ok(await _taskService.AddTaskAsync(taskToAddDTO));
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
        [HttpPut("updatestatus/{taskId}")]
        public async Task<IActionResult> UpdateStatusTask(int taskId)
        {
            try
            {
                return Ok(await _taskService.UpdateStatusTaskAsync(taskId));
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error("KeyNotFoundException in UpdateStatusTask controller", ex);
                return NotFound("Task not found");
            }
            catch (Exception ex)
            {
                Log.Error( "An unexpected error occurred in UpdateStatusTask controller", ex);
                return BadRequest("Something went wrong");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpPut("updatetask")]
        public async Task<IActionResult> UpdateTask(TaskDTO taskToUpdate)
        {
            try
            {
                return Ok(await _taskService.UpdateTaskAsync(taskToUpdate));
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error("KeyNotFoundException in UpdateTask controller", ex);
                return NotFound("Task not found");
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in UpdateTasks controller", ex);
                return BadRequest("Something went wrong");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpDelete("deletetask/{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            try
            {
                await _taskService.DeleteTaskAsync(taskId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error("KeyNotFoundException in DeleteTaskAsync controller", ex);
                return NotFound("Task not found");
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in DeleteTaskAsync controller", ex);
                return BadRequest("Something went wrong");
            }
        }

    }
}
