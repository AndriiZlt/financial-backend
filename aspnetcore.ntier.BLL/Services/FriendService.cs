using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.BLL.Utilities.CustomExceptions;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Serilog;

namespace aspnetcore.ntier.BLL.Services;

public class FriendService : IFriendService
{
    private readonly IFriendRepository _friendRepository;
    private readonly IMapper _mapper;

    public FriendService(IFriendRepository friendRepository, IMapper mapper)
    {
        _friendRepository = friendRepository;
        _mapper = mapper;
    }

    public async Task<List<FriendDTO>> GetFriendsAsync(CancellationToken cancellationToken = default)
    {
        var friendsToReturn = await _friendRepository.GetListAsync(cancellationToken: cancellationToken);
        Log.Information("List of {Count} friends has been returned", friendsToReturn.Count);
         
        return _mapper.Map<List<FriendDTO>>(friendsToReturn);
    }


    public async Task<FriendDTO> AddFriendAsync(FriendToAddDTO friendToAddDTO)
    {
        var addedUser = await _friendRepository.AddAsync(_mapper.Map<Friend>(friendToAddDTO));

        return _mapper.Map<FriendDTO>(addedUser);
    }

    public async Task DeleteFriendAsync(int friendId)
    {
        var friendToDelete = await _friendRepository.GetAsync(x => x.Id == friendId);

        if (friendToDelete is null)
        {
            Log.Information("Friend with Id = {Id} was not found", friendId);
            throw new UserNotFoundException();
        }

        await _friendRepository.DeleteAsync(friendToDelete);
    }
}