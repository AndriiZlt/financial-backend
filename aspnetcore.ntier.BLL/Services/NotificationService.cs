﻿using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.DataContext;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using aspnetcore.ntier.BLL.Utilities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using aspnetcore.ntier.DAL.Repositories;



namespace aspnetcore.ntier.BLL.Services
{
    public class NotificationService : INotificationService
    {

        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        private readonly IHubContext<NotificationHub> _hubContext;
        private readonly IConnectionService _connectionService;
/*        private readonly IApplicationBuilder app;*/
        private readonly IMemoryCache _memoryCache;
        private readonly IGenericRepository<Notification> _genericRepository;

        public NotificationService(
            INotificationRepository notificationRepository,
            IMapper mapper,
            IHttpContextAccessor httpContext,
            AspNetCoreNTierDbContext aspNetCoreNTierDbContext,
            IHubContext<NotificationHub> hubContext,
            IConnectionService connectionService,
            IMemoryCache memoryCache,
            IGenericRepository<Notification> genericRepository
            )
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
            _hubContext = hubContext;
            _connectionService = connectionService;
            _memoryCache = memoryCache;
            _genericRepository = genericRepository;
        }

        public async Task<List<NotificationDTO>> GetNotificationsAsync(CancellationToken cancellationToken = default)
        {
            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var notificationsToReturn = await _notificationRepository.GetListAsync(Int32.Parse(userId));
            return _mapper.Map<List<NotificationDTO>>(notificationsToReturn);
        }


        public async Task<NotificationDTO> AddNotificationAsync([FromBody] NotificationToAddDTO notificationToAdd)
        {

            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var notificationToReturn = await _notificationRepository.AddAsync(_mapper.Map<Notification>(notificationToAdd));
            Log.Information("Added notification: {@not}", notificationToAdd);
            return _mapper.Map<NotificationDTO>(notificationToReturn);

        }

        public async Task<NotificationDTO> CreateNotificationsFromTransaction(DAL.Entities.Transaction transaction)
        {
            NotificationToAddDTO sellerNotificationToAdd = new NotificationToAddDTO()
            {
                User_Id = transaction.Seller_User_Id,
                Type = NotificationType.Sell_Transaction,
                Text = $"You sold {transaction.Symbol} stock. Qty:{transaction.Qty}. Total price: ${transaction.Total_Price}"
            };

            NotificationToAddDTO buyerNotificationToAdd = new NotificationToAddDTO()
            {
                User_Id = transaction.Buyer_User_Id,
                Type = NotificationType.Buy_Transaction,
                Text = $"You bought {transaction.Symbol} stock. Qty:{transaction.Qty}. Total price: ${transaction.Total_Price}"
            };

            var sellerNot= await _notificationRepository.AddAsync(_mapper.Map<Notification>(sellerNotificationToAdd));
            SendNotificationsUpdate(transaction.Seller_User_Id.ToString());
            var buyerNot = await _notificationRepository.AddAsync(_mapper.Map<Notification>(buyerNotificationToAdd));
            SendNotificationsUpdate(transaction.Buyer_User_Id.ToString());
            Log.Information("Seller's notification created: {@not}", _mapper.Map<NotificationDTO>(sellerNot));
            Log.Information("Buyer's notification created: {@not}", _mapper.Map<NotificationDTO>(buyerNot));
            return _mapper.Map<NotificationDTO>(buyerNot);

        }

        public async void SendNotificationsUpdate(string userId)
        {
            var key = $"UserId_{userId}";

            List<string> userConnections = _memoryCache.Get<List<string>>(key);
            Log.Information("{key} Connections : {@id}", key, userConnections);
            if (userConnections != null)
            {
                foreach (var connection in userConnections)
                {
                    string connectionId = connection.Split('=')[1];
                    Log.Information("NotificationHub NewNotification: {id}", connectionId);
                    await _hubContext.Clients.Client(connectionId).SendAsync("NewNotification", "Update notifications");

                }
            }

        }

        public async Task<NotificationDTO> ReadNotificationAsync(string notificationId, CancellationToken cancellationToken = default)
        {

            var NotificationkToUpdate = await _genericRepository.GetAsync(x => x.Id == Int32.Parse(notificationId), cancellationToken );

            if (NotificationkToUpdate is null)
            {
                Log.Information("Notification with Id = {Id} was not found", notificationId);
                throw new KeyNotFoundException();
            }

            NotificationkToUpdate.Status = NotificationStatus.Read;

            Log.Information("Updated Notification {@stock}", _mapper.Map<NotificationDTO>(NotificationkToUpdate));

            var userId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            SendNotificationsUpdate(userId.ToString());

            return _mapper.Map<NotificationDTO>(await _genericRepository.UpdateAsync(NotificationkToUpdate));
        }
    }
}
