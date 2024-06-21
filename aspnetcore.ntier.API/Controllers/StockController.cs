
using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.Entities;
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
    public class StockController : ControllerBase
    {

        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpGet("getstocks")]
        public async Task<IActionResult> GetStocks()
        {
            try
            {
                var result = await _stockService.GetStocksAsync();
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
        [HttpPost("addstock")]
        public async Task<IActionResult> AddStock(StockToAddDTO stockToAddDTO)
        {
            try
            {
                return Ok(await _stockService.AddStockAsync(stockToAddDTO));
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
        [HttpPut("updatestatus/{stockId}/{newStatus}")]
        public async Task<IActionResult> UpdateStockStatus(int stockId, string newStatus)
        {
            try
            {
                Log.Information("StockId:{1}; New Status:{2}", stockId, newStatus);
                return Ok(await _stockService.UpdateStatusTaskAsync(stockId, newStatus));
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error("KeyNotFoundException in UpdateStockStatus controller", ex);
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
        [HttpPut("updatestock")]
        public async Task<IActionResult> UpdatezStock(StockDTO updatedStock)
        {
            try
            {
                Log.Information("Updated stock:{@1}", updatedStock);
                return Ok(await _stockService.UpdateStockAsync(updatedStock));
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
        public async Task<IActionResult> DeleteStock(int taskId)
        {
            try
            {
                await _stockService.DeleteTaskAsync(taskId);
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
