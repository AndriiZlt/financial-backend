
using aspnetcore.ntier.DTO.DTOs;

namespace aspnetcore.ntier.BLL.Services.IServices;

public interface IBoardService
{
    Task<List<BoardItemDTO>> GetBoardAsync(CancellationToken cancellationToken = default);

    Task<BoardItemDTO> AddToBoardAsync(BoardItemToAddDTO stockToAddDTO);

    Task DeleteBoardItemAsync(int stock_Id);

    Task UpdateFrontent(int userId);
}
