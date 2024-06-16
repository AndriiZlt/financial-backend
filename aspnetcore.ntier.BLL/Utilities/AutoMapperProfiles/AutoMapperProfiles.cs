﻿using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;

namespace aspnetcore.ntier.BLL.Utilities.AutoMapperProfiles;

public static class AutoMapperProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserToAddDTO>().ReverseMap();
            CreateMap<User, UserToUpdateDTO>().ReverseMap();
            CreateMap<User, UserToRegisterDTO>().ReverseMap();
            CreateMap<User, UserToReturnDTO>().ReverseMap();
            CreateMap<Taskk, TaskDTO>().ReverseMap();
            CreateMap<Taskk, TaskToAddDTO>().ReverseMap();
            CreateMap<Subtask, SubtaskDTO>().ReverseMap();
            CreateMap<Subtask, SubtaskToAddDTO>().ReverseMap();
            CreateMap<Friend, FriendDTO>().ReverseMap();
            CreateMap<Friend, FriendToAddDTO>().ReverseMap();
            CreateMap<Stock, StockToAddDTO>().ReverseMap();
            CreateMap<Stock, StockDTO>().ReverseMap();
            CreateMap<BoardItem, BoardItemDTO>().ReverseMap();
            CreateMap<BoardItem, BoardItemToAddDTO>().ReverseMap();
        }
    }
}
