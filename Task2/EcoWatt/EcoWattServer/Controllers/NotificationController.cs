using System.Security.Claims;
using BLL.Notifications;
using Core.DTOs.Error;
using Core.DTOs.Notification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoWattServer.Controllers;

[Authorize]
[ApiController]
[Route("Api/[controller]")]
public class NotificationController : Controller
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationById(int id)
    {
        var result = await _notificationService.GetNotificationByIdAsync(id);

        if (result is ErrorDto)
        {
            var errorResult = result as ErrorDto;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNotifications()
    {
        var result = await _notificationService.GetAllNotificationsAsync();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] NotificationModel notificationModel)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await _notificationService.CreateNotificationAsync(userId, notificationModel);

        if (result is ErrorDto)
        {
            var errorResult = result as ErrorDto;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNotification(int id, [FromBody] NotificationModel notificationModel)
    {
        var result = await _notificationService.UpdateNotificationAsync(id, notificationModel);

        if (result is ErrorDto)
        {
            var errorResult = result as ErrorDto;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        var result = await _notificationService.DeleteNotificationAsync(id);

        if (result is SuccessDto)
        {
            return NoContent();
        }

        var errorResult = result as ErrorDto;

        return BadRequest(errorResult.Message);
    }
}