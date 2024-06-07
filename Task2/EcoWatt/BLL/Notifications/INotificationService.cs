using Core.DTOs;
using Core.DTOs.Notification;

namespace BLL.Notifications;

public interface INotificationService
{
    Task<IModel> GetNotificationByIdAsync(int notificationId);

    Task<List<NotificationModel>> GetAllNotificationsAsync();

    Task<IModel> CreateNotificationAsync(int userId, NotificationModel notificationDto);

    Task<IModel> UpdateNotificationAsync(int notificationId, NotificationModel notificationDto);

    Task<IModel> DeleteNotificationAsync(int notificationId);
}