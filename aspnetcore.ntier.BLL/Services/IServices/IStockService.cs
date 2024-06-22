
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.ntier.BLL.Services.IServices;

public interface IStockService
{
    Task<List<StockDTO>> GetStocksAsync(CancellationToken cancellationToken = default);

    Task<List<StockDTO>> GetBoardAsync(CancellationToken cancellationToken = default);

    Task<StockDTO> AddStockAsync(StockToAddDTO stockToAddDTO);

    Task DeleteTaskAsync(int taskId);

    Task<StockDTO> UpdateStatusTaskAsync(int stockId, StockStatus status);

    Task<StockDTO> UpdateStockAsync(StockDTO stock);
}
