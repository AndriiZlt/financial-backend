using aspnetcore.ntier.DTO.DTOs;

namespace aspnetcore.ntier.BLL.Services.IServices;

public interface IFriendService
{
    Task<List<FriendDTO>> GetFriendsAsync(CancellationToken cancellationToken = default);

    Task<FriendDTO> AddFriendAsync(FriendToAddDTO friendToAddDTO);

    Task DeleteFriendAsync(int friendId);
}
