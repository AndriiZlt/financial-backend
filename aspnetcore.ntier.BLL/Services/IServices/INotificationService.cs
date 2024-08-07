﻿using aspnetcore.ntier.DAL.Entities;
using aspnetcore.ntier.DTO.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcore.ntier.BLL.Services.IServices
{
    public interface INotificationService
    {
        Task<List<NotificationDTO>> GetNotificationsAsync(CancellationToken cancellationToken = default);
        Task<NotificationDTO> AddNotificationAsync([FromBody] NotificationToAddDTO notificationToAdd);
        Task<NotificationDTO> CreateNotificationsFromTransaction(DAL.Entities.Transaction transaction);
        Task<NotificationDTO> CreateNotificationsFromAlpacaTransaction(AlpacaTransaction transaction);
        Task<NotificationDTO> ReadNotificationAsync(string notificationId, CancellationToken cancellationToken = default);
        Task SendNotificationsUpdate(string userId);
    }
}