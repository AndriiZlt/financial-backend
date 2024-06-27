
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.ntier.BLL.Services.IServices;

public interface IStockService
{
    Task<List<StockDTO>> GetStocksAsync(CancellationToken cancellationToken = default);

    Task<StockDTO> AddStockAsync(StockToAddDTO stockToAddDTO);

    Task<StockDTO> UpdateStatusAsync(int stockId, StockStatus status);

/*    Task<StockDTO> UpdateStockAsync(StockDTO stock);*/

    Task<StockDTO> BuyStockAsync(string stockId);
}
