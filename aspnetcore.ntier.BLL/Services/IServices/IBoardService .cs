
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;

namespace aspnetcore.ntier.BLL.Services.IServices;

public interface IBoardService
{
    Task<List<BoardStockDTO>> GetBoardAsync(CancellationToken cancellationToken = default);

    Task<BoardStockDTO> AddToBoardAsync(BoardAddDTO stockToAddDTO);

    Task RemoveFromBoardAsync(int stockId);

/*    Task<SubtaskDTO> UpdateStatusSubtaskAsync(int taskId);

    Task<SubtaskDTO> UpdateSubtaskAsync(SubtaskDTO taskToUpdate);*/
}
