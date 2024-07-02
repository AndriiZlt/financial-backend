
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.ntier.BLL.Services.IServices;

public interface IStockService
{
    Task<List<StockDTO>> GetStocksAsync(CancellationToken cancellationToken = default);
    Task<List<StockDTO>> GetStocksAsync(int userId, CancellationToken cancellationToken = default);
    Task<StockDTO> GetStockAsync(int stockId, CancellationToken cancellationToken = default);
    Task<StockDTO> AddStockAsync(StockToAddDTO stockToAddDTO);
    Task<StockDTO> UpdateOrAddNewStockAsync([FromBody] StockToAddDTO stockToAddDTO);
    Task<StockDTO> AddEmptyStockAsync([FromBody] StockToAddDTO stockToAddDTO);
    Task<StockDTO> UpdateStatusAsync(int stockId, StockStatus status);
    Task<StockDTO> UpdateStockAsync(int stockId, StockDTO stockForUpdate);
    Task<StockDTO> BuyStockAsync(string stockId);
}
