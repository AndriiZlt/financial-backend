﻿using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.BLL.Utilities.CustomExceptions;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Security.Claims;

namespace aspnetcore.ntier.BLL.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContext;

    public UserService(IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContext)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _httpContext = httpContext;
    }

    public async Task<List<UserDTO>> GetUsersAsync(CancellationToken cancellationToken = default)
    {
        var usersToReturn = await _userRepository.GetListAsync(cancellationToken: cancellationToken);
        Log.Information("List of {Count} users has been returned", usersToReturn.Count);
         
        return _mapper.Map<List<UserDTO>>(usersToReturn);
    }

    public async Task<UserDTO> GetUserAsync(CancellationToken cancellationToken = default)
    {
        var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userToReturn = await _userRepository.GetAsync(x => x.Id == Int32.Parse(userId), cancellationToken);

        if (userToReturn is null)
        {
            Log.Information("User with userId = {UserId} was not found", userId);
            throw new UserNotFoundException();
        }

        return _mapper.Map<UserDTO>(userToReturn);
    }

    public async Task<string> GetUserBallanceAsync(int userId, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetAsync(x => x.Id == userId, cancellationToken);

        if (user is null)
        {
            Log.Information("User with userId = {UserId} was not found", userId);
            throw new UserNotFoundException();
        }
        return user.Ballance;
    }

    public async Task<string> GetUserBallanceAsync(CancellationToken cancellationToken = default)
    {
        var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        var user = await _userRepository.GetAsync(x => x.Id.ToString() == userId, cancellationToken);

        if (user is null)
        {
            Log.Information("User with userId = {UserId} was not found", userId);
            throw new UserNotFoundException();
        }
        Log.Information("Get ballance for User {UserId} = {@ballance}", userId, user.Ballance);
        return user.Ballance;
    }

    public async Task<string> UpdateUserBallanceAsync(int userId, float newBallance, CancellationToken cancellationToken = default)
    {
        var userBeforeUpdate = await _userRepository.GetAsync(x => x.Id == userId, cancellationToken);

        if (userBeforeUpdate is null)
        {
            Log.Information("User with userId = {UserId} was not found", userId);
            throw new UserNotFoundException();
        }

        userBeforeUpdate.Ballance = newBallance.ToString(); 
      
        var userAfterUpdate= _mapper.Map<User>(userBeforeUpdate);

        var updatedUser = await _userRepository.UpdateUserAsync(userAfterUpdate);

        Log.Information("Updated ballance for User {UserId} = {ballance}", updatedUser.Id, updatedUser.Ballance);

        return updatedUser.Ballance;
    }

    public async Task<UserDTO> AddUserAsync(UserToAddDTO userToAddDTO)
    {
        userToAddDTO.UserName = userToAddDTO.UserName.ToLower();
        var addedUser = await _userRepository.AddAsync(_mapper.Map<User>(userToAddDTO));

        return _mapper.Map<UserDTO>(addedUser);
    }

    public async Task<UserDTO> UpdateUserAsync(UserToUpdateDTO userToUpdateDTO)
    {
        userToUpdateDTO.UserName = userToUpdateDTO.UserName.ToLower();
        var user = await _userRepository.GetAsync(x => x.Id == userToUpdateDTO.Id);

        if (user is null)
        {
            Log.Information("User with userId = {UserId} was not found", userToUpdateDTO.Id);
            throw new UserNotFoundException();
        }

        var userToUpdate = _mapper.Map<User>(userToUpdateDTO);

        Log.Information("User with these properties: {@UserToUpdate} has been updated", userToUpdateDTO);

        return _mapper.Map<UserDTO>(await _userRepository.UpdateUserAsync(userToUpdate));
    }

    public async Task DeleteUserAsync(int userId)
    {
        var userToDelete = await _userRepository.GetAsync(x => x.Id == userId);

        if (userToDelete is null)
        {
            Log.Information("User with userId = {UserId} was not found", userId);
            throw new ArgumentOutOfRangeException();
        }

        await _userRepository.DeleteAsync(userToDelete);
    }


}