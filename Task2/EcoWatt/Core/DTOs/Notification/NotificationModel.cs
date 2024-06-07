namespace Core.DTOs.Notification;

public class NotificationModel: IModel
{
    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; }
}