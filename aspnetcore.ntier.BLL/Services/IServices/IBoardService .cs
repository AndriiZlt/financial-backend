
using aspnetcore.ntier.DTO.DTOs;

namespace aspnetcore.ntier.BLL.Services.IServices;

public interface IBoardService
{
    Task<List<BoardItemDTO>> GetBoardAsync(CancellationToken cancellationToken = default);

    Task<BoardItemDTO> AddToBoardAsync(BoardItemToAddDTO stockToAddDTO);

/*    Task RemoveFromBoardAsync(int stockId);*/

/*    Task<SubtaskDTO> UpdateStatusSubtaskAsync(int taskId);

    Task<SubtaskDTO> UpdateSubtaskAsync(SubtaskDTO taskToUpdate);*/
}
