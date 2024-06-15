
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;

namespace aspnetcore.ntier.BLL.Services.IServices;

public interface IStockService
{
    Task<List<StockDTO>> GetStocksAsync(CancellationToken cancellationToken = default);

    Task<StockDTO> AddStockAsync(StockToAddDTO stockToAddDTO);

    Task DeleteTaskAsync(int taskId);

/*    Task<TaskDTO> UpdateStatusTaskAsync(int taskId);

    Task<TaskDTO> UpdateTaskAsync(TaskDTO taskToUpdate);*/
}
