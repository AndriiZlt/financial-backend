using Microsoft.AspNetCore.Mvc;
using aspnetcore.ntier.DTO.DTOs;
using aspnetcore.ntier.BLL.Services.IServices;
using Serilog;


namespace aspnetcore.ntier.API.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class AlpacaController : ControllerBase
{
    private readonly IAlpacaService _alpacaService;
    public AlpacaController(IAlpacaService alpacaService) {
    _alpacaService = alpacaService;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(ValidationProblemDetails))]
    [HttpGet("assets")]
    public async Task<IActionResult> GetAssets()
    {
        try
        {
            Request.Headers.TryGetValue("apca-api-key-id", out var keyId);
            Request.Headers.TryGetValue("apca-api-secret-key", out var secretKey);
            return Ok(await _alpacaService.GetAssetsAsync(keyId,secretKey));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetAssets controller", ex);
            return BadRequest("Something went wrong");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("asset/{assetId}")]
    public async Task<IActionResult> GetAsset(string assetId)
    {
        try
        {
            Request.Headers.TryGetValue("apca-api-key-id", out var keyId);
            Request.Headers.TryGetValue("apca-api-secret-key", out var secretKey);
            return Ok(await _alpacaService.GetAssetAsync(keyId, secretKey, assetId));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetAsset controller", ex);
            return BadRequest("Something went wrong");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("positions")]
    public async Task<IActionResult> GetPositions()
    {
        try
        {
            Request.Headers.TryGetValue("apca-api-key-id", out var keyId);
            Request.Headers.TryGetValue("apca-api-secret-key", out var secretKey);
            return Ok(await _alpacaService.GetPositionsAsync(keyId, secretKey));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetPositions controller", ex);

            return BadRequest("Something went wrong");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpDelete("position/{asset_id}")]
    public async Task<IActionResult> ClosePosition(string asset_id)
    {
        try
        {
            Request.Headers.TryGetValue("apca-api-key-id", out var keyId);
            Request.Headers.TryGetValue("apca-api-secret-key", out var secretKey);
            return Ok(await _alpacaService.ClosePositionAsync(keyId, secretKey, asset_id));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in ClosePosition controller", ex);

            return BadRequest("Something went wrong");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions()
    {
        try
        {
            Request.Headers.TryGetValue("apca-api-key-id", out var keyId);
            Request.Headers.TryGetValue("apca-api-secret-key", out var secretKey);
            return Ok(await _alpacaService.GetTransactionsAsync(keyId, secretKey));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetTransactions controller", ex);

            return BadRequest("Something went wrong");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("bars/{symbol}")]
    public async Task<IActionResult> GetMonthBars(string symbol)
    {
        try
        {
            Request.Headers.TryGetValue("apca-api-key-id", out var keyId);
            Request.Headers.TryGetValue("apca-api-secret-key", out var secretKey);
            return Ok(await _alpacaService.GetMonthBarsAsync(keyId, secretKey, symbol));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetMonthBars controller", ex);

            return BadRequest("Something went wrong");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("account")]
    public async Task<IActionResult> GetAccount()
    {
        try
        {
            Request.Headers.TryGetValue("apca-api-key-id", out var keyId);
            Request.Headers.TryGetValue("apca-api-secret-key", out var secretKey);
            return Ok(await _alpacaService.GetAccountAsync(keyId, secretKey));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetAccount controller", ex);

            return BadRequest("Something went wrong");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("orders")]
    public async Task<IActionResult> GetOrders()
    {
        try
        {
            Request.Headers.TryGetValue("apca-api-key-id", out var keyId);
            Request.Headers.TryGetValue("apca-api-secret-key", out var secretKey);
            return Ok(await _alpacaService.GetOrdersAsync(keyId, secretKey));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetOrders controller", ex);

            return BadRequest("Something went wrong");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpPost("order")]
    public async Task<IActionResult> CreateOrder(OrderDTO order)
    {
        try
        {
            Request.Headers.TryGetValue("apca-api-key-id", out var keyId);
            Request.Headers.TryGetValue("apca-api-secret-key", out var secretKey);
            return Ok(await _alpacaService.CreateOrdersAsync(keyId, secretKey, order));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in CreateOrder controller", ex);

            return BadRequest("Something went wrong");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("trades/{symbol}")]
    public async Task<IActionResult> GetTrades(string symbol)
    {
        try
        {
            Request.Headers.TryGetValue("apca-api-key-id", out var keyId);
            Request.Headers.TryGetValue("apca-api-secret-key", out var secretKey);
            return Ok(await _alpacaService.GetTradesAsync(keyId, secretKey, symbol));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetTrades controller", ex);
            return BadRequest("Something went wrong");
        }
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ValidationProblemDetails))]
    [HttpGet("lastbar/{symbol}")]
    public async Task<IActionResult> GetLastBar(string symbol)
    {
        try
        {
            Request.Headers.TryGetValue("apca-api-key-id", out var keyId);
            Request.Headers.TryGetValue("apca-api-secret-key", out var secretKey);
            return Ok(await _alpacaService.GetLastBarAsync(keyId, secretKey, symbol));
        }
        catch (Exception ex)
        {
            Log.Error("An unexpected error occurred in GetLastBar controller", ex);
            return BadRequest("Something went wrong");
        }
    }
}
