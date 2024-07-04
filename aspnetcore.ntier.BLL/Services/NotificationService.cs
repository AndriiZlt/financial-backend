using aspnetcore.ntier.BLL.Services.IServices;
using aspnetcore.ntier.DAL.DataContext;
using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DAL.Repositories.IRepositories;
using aspnetcore.ntier.DTO.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;
using System.Transactions;



namespace aspnetcore.ntier.BLL.Services
{
    public class NotificationService : INotificationService
    {

        private readonly INotificationRepository _notificationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AspNetCoreNTierDbContext _aspNetCoreNTierDbContext;
        public NotificationService(
            INotificationRepository notificationRepository,
            IMapper mapper,
            IHttpContextAccessor httpContext,
            AspNetCoreNTierDbContext aspNetCoreNTierDbContext
            )
        {
            _notificationRepository = notificationRepository;
            _mapper = mapper;
            _httpContext = httpContext;
            _aspNetCoreNTierDbContext = aspNetCoreNTierDbContext;
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
                Text = $"You sold {transaction.Symbol} stock. Qty:{transaction.Qty}. Total price: {transaction.Total_Price}."
            };

            NotificationToAddDTO buyerNotificationToAdd = new NotificationToAddDTO()
            {
                User_Id = transaction.Buyer_User_Id,
                Type = NotificationType.Buy_Transaction,
                Text = $"You bought {transaction.Symbol} stock. Qty:{transaction.Qty}. Total price: {transaction.Total_Price}."
            };

            var sellerNot= await _notificationRepository.AddAsync(_mapper.Map<Notification>(sellerNotificationToAdd));
            var buyerNot = await _notificationRepository.AddAsync(_mapper.Map<Notification>(buyerNotificationToAdd));
            Log.Information("Seller's notification created: {@not}", _mapper.Map<NotificationDTO>(sellerNot));
            Log.Information("Buyer's notification created: {@not}", _mapper.Map<NotificationDTO>(buyerNot));
            return _mapper.Map<NotificationDTO>(buyerNot);

        }
    }
}
