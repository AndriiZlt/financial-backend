using aspnetcore.ntier.DAL.Entities;
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
            CreateMap<Stock, StockToAddDTO>().ReverseMap();
            CreateMap<Stock, StockDTO>().ReverseMap();
            CreateMap<BoardItem, BoardItemDTO>().ReverseMap();
            CreateMap<BoardItem, BoardItemToAddDTO>().ReverseMap();
            CreateMap<Transaction, TransactionToAddDTO>().ReverseMap();
            CreateMap<Transaction, TransactionDTO>().ReverseMap();
            CreateMap<TransactionToAddDTO, TransactionFrontendDTO>().ReverseMap();
        }
    }
}
