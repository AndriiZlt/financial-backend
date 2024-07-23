using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.BLL.Utilities;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Serilog;
using System;

namespace aspnetcore.ntier.BLL.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IStockService _stockService;
        private readonly IMemoryCache _memoryCache;
        private readonly IHubContext<NotificationHub> _hubContext;

        public BoardService (
            IBoardRepository boardRepository, 
            IMapper mapper, 
            IHttpContextAccessor httpContext, 
            IStockService stockService, 
            IMemoryCache memoryCache,
            IHubContext<NotificationHub> hubContext
            )
        {
            _boardRepository = boardRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            _stockService = stockService;
            _memoryCache = memoryCache;
            _hubContext= hubContext;
        }

        public async Task<List<BoardItemDTO>> GetBoardAsync(CancellationToken cancellationToken = default)
        {
            var itemsToReturn = await _boardRepository.GetBoardListAsync();
            return _mapper.Map<List<BoardItemDTO>>(itemsToReturn);
        }

        public async Task<BoardItemDTO> AddToBoardAsync([FromBody] BoardItemToAddDTO boardItemToAdd)
        {;
            var addedItem = await _boardRepository.AddAsync(_mapper.Map<BoardItem>(boardItemToAdd));
            if(boardItemToAdd.Status == StockStatus.For_Sale)
            {
                _ = await _stockService.UpdateStatusAsync(addedItem.Stock_Id, boardItemToAdd.Status);
            }
            Log.Information("Added item: {@item}", _mapper.Map<BoardItemDTO>(addedItem));
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
            await _stockService.UpdateStatusAsync(itemToDelete.Stock_Id, StockStatus.Fixed);

            await UpdateFrontent(itemToDelete.User_Id);
        }

        public async Task UpdateFrontent(int userId)
        {
            Log.Information("UpdateFrontend user: {id}", userId);
            var key = $"UserId_{userId}";

            List<string> userConnections = _memoryCache.Get<List<string>>(key);
            Log.Information("User connetctions for message : {@userConnections}", userConnections);
            if (userConnections != null)
            {
                foreach (var connection in userConnections)
                {
                    string connectionId = connection.Split('=')[1];
                    Log.Information("Message for connectionId : {@id}", connectionId);
                    await _hubContext.Clients.Client(connectionId).SendAsync("PublicBoard", "Public Board update");
                }
            }

        }

    }
}

