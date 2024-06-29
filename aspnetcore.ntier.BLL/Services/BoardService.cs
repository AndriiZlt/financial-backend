using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Security.Claims;



namespace aspnetcore.ntier.BLL.Services
{
    public class BoardService : IBoardService
    {

        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IStockService _stockService;


        public BoardService (IBoardRepository boardRepository, IMapper mapper,  IHttpContextAccessor httpContext, IStockService stockService)
        {
            _boardRepository = boardRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            _stockService = stockService;
        }

        public async Task<List<BoardItemDTO>> GetBoardAsync(CancellationToken cancellationToken = default)
        {
            var itemsToReturn = await _boardRepository.GetBoardListAsync();
            return _mapper.Map<List<BoardItemDTO>>(itemsToReturn);
        }


        public async Task<BoardItemDTO> AddToBoardAsync([FromBody] BoardItemToAddDTO boardItemToAdd)
        {
            Log.Information("boardItemToAdd = {@UserId}", boardItemToAdd);
            var addedItem = await _boardRepository.AddAsync(_mapper.Map<BoardItem>(boardItemToAdd));
            if(boardItemToAdd.Status == StockStatus.For_Sale)
            {
                var updatedStock = await _stockService.UpdateStatusAsync(addedItem.Stock_Id, boardItemToAdd.Status);
            }

            return _mapper.Map<BoardItemDTO>(addedItem);
        }

        public async Task DeleteBoardItemAsync(int boardItemId)
        {
            var itemToDelete = await _boardRepository.GetAsync(x => x.Id == boardItemId);

            if (itemToDelete is null)
            {
                Log.Information("BoardItem with Id {UserId} was not found", boardItemId);
                throw new KeyNotFoundException();
            }

            await _boardRepository.DeleteAsync(itemToDelete);
        }




    }
}

