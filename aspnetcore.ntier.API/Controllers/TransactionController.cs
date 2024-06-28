
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

    public class TransactionController : ControllerBase
    {

        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }



        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpGet("gettransactions")]
        public async Task<IActionResult> GetTransactions()
        {
            try
            {
                var result = await _transactionService.GetTransactionsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in GetTransactions controller. {@ex}", ex.Message);
                return BadRequest("Something went wrong");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpPost("addtransaction")]
        public async Task<IActionResult> AddTransaction(TransactionToAddDTO transactionToAdd)
        {
            try
            {
                return Ok(await _transactionService.AddTransactionAsync(transactionToAdd));
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in AddTransaction controller. {@ex}", ex.Message);
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
