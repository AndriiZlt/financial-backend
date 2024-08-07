﻿
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
                Log.Error("An unexpected error occurred in GetTasks controller. {@ex}", ex.Message);
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
                Log.Error("An unexpected error occurred in AddTask controller. {@ex}", ex.Message);
                return BadRequest("Something went wrong");
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpPost("createstock")]
        public async Task<IActionResult> CreateEmptyStock(StockToAddDTO stockToAddDTO)
        {
            try
            {
                Log.Information("--- New Buy order --- ");
                return Ok(await _stockService.AddEmptyStockAsync(stockToAddDTO));
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in CreateEmptyStockAsyn controller. {@ex}", ex.Message);
                return BadRequest("Something went wrong");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpPut("updatestatus/{stockId}/{newStatus}")]
        public async Task<IActionResult> UpdateStockStatus(int stock_Id, StockStatus newStatus)
        {
            try
            {
                Log.Information("StockId:{1}; New Status:{2}", stock_Id, newStatus);
                return Ok(await _stockService.UpdateStatusAsync(stock_Id, newStatus));
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error("KeyNotFoundException in UpdateStockStatus controller. {@ex}", ex.Message);
                return NotFound("Task not found");
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in UpdateStatusTask controller", ex.Message);
                return BadRequest("Something went wrong");
            }
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
        [HttpPut("buystock/{stockId}")]
        public async Task<IActionResult> BuyStock(string stockId)
        {
            try
            {
                return Ok(await _stockService.BuyStockAsync(stockId));
            }
            catch (KeyNotFoundException ex)
            {
                Log.Error("KeyNotFoundException in UpdateTask controller. {@ex}", ex.Message);
                return NotFound("Task not found");
            }
            catch (Exception ex)
            {
                Log.Error("An unexpected error occurred in UpdateTasks controller. {@ex}", ex.Message);
                return BadRequest("Something went wrong");
            }
        }

    }
}
