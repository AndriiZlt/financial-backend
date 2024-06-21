
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

    public class BoardController : ControllerBase
    {

        private readonly IBoardService _boardService;

        public BoardController(IBoardService boardService)
        {
            _boardService = boardService;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpGet("getboard")]
        public async Task<IActionResult> GetBoard()
        {
            try
            {
                var result = await _boardService.GetBoardAsync();
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
        [HttpPost("addboard")]
        public async Task<IActionResult> AddToBoard(BoardItemToAddDTO boardItemToAdd)
        {
            try
            {
                
                return Ok(await _boardService.AddToBoardAsync(boardItemToAdd));
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in AddTask controller", ex);
                return BadRequest("Something went wrong");
            }
        }


        /*[ProducesResponseType(StatusCodes.Status200OK)]
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
        }*/
       


        /*[ProducesResponseType(StatusCodes.Status200OK)]
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
        }*/


        /*[ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpDelete("removeboard")]
        public async Task<IActionResult> RemoveItemFromBoard(int stockId)
        {
            try
            {
                await _boardService.RemoveFromBoardAsync(stockId);
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
        }*/

    }
}
