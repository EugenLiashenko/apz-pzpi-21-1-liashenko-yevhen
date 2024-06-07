using Core.DTOs;
using Core.DTOs.Error;
using Core.DTOs.Notification;
using Core.Entities;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace BLL.Notifications;

public class NotificationService: INotificationService
{
    private readonly DataContext _dataContext;

        public NotificationService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IModel> CreateNotificationAsync(int userId, NotificationModel notificationModel)
        {
            var dbUser = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (dbUser is null)
            {
                return new ErrorDto()
                {
                    Message = $"User with id: {userId} was not found"
                };
            }

            var notification = new Notification
            {
                Message = notificationModel.Message,
                IsRead = notificationModel.IsRead,
                User = dbUser
            };

            _dataContext.Notifications.Add(notification);
            await _dataContext.SaveChangesAsync();

            return new NotificationModel
            {
                Message = notification.Message,
                IsRead = notification.IsRead
            };
        }

        public async Task<IModel> DeleteNotificationAsync(int id)
        {
            var notification = await _dataContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);

            if (notification is null)
            {
                return new ErrorDto()
                {
                    Message = $"Notification with id {id} not found."
                };
            }

            _dataContext.Notifications.Remove(notification);
            await _dataContext.SaveChangesAsync();

            return new SuccessDto()
            {
                Message = "Notification was successfully deleted."
            };
        }

        public async Task<List<NotificationModel>> GetAllNotificationsAsync()
        {
            var notifications = await _dataContext.Notifications.ToListAsync();

            return notifications.Select(n => new NotificationModel
            {
                Message = n.Message,
                IsRead = n.IsRead
            }).ToList();
        }

        public async Task<IModel> GetNotificationByIdAsync(int id)
        {
            var notification = await _dataContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);

            if (notification is null)
            {
                return new ErrorDto()
                {
                    Message = $"Notification with id: {id} not found."
                };
            }

            return new NotificationModel
            {
                Message = notification.Message,
                IsRead = notification.IsRead
            };
        }

        public async Task<IModel> UpdateNotificationAsync(int id, NotificationModel notificationModel)
        {
            var dbNotification = await _dataContext.Notifications.FirstOrDefaultAsync(n => n.Id == id);

            if (dbNotification is null)
            {
                return new ErrorDto()
                {
                    Message = $"Notification with id {id} not found."
                };
            }

            dbNotification.Message = notificationModel.Message;
            dbNotification.IsRead = notificationModel.IsRead;

            await _dataContext.SaveChangesAsync();

            return new NotificationModel
            {
                Message = dbNotification.Message,
                IsRead = dbNotification.IsRead
            };
        }
}