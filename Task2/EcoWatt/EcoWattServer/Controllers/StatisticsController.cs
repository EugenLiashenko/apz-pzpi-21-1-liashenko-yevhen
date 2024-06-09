using BLL.Statistics;
using EcoWattServer.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoWattServer.Controllers;

[Authorize]
[AdminAuth]
[ApiController]
[Route("Api/[controller]")]
public class StatisticsController : Controller
{
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }
    
    [HttpGet("BookingsStartCountPerDayLastMonth")]
    public async Task<IActionResult> GetBookingsStartCountPerDayLastMonth()
    {
        var result = await _statisticsService.GetBookingsStartCountPerDayLastMonthAsync();
        
        return Ok(result);
    }

    [HttpGet("BookingsEndCountPerDayLastMonth")]
    public async Task<IActionResult> GetBookingsEndCountPerDayLastMonth()
    {
        var result = await _statisticsService.GetBookingsEndCountPerDayLastMonthAsync();
        
        return Ok(result);
    }
    
    [HttpGet("BookingCountLastWeek")]
    public async Task<IActionResult> GetBookingCountLastWeek()
    {
        var result = await _statisticsService.GetBookingsLastWeekAsync();
        
        return Ok(result);
    }

    [HttpGet("AverageBookingsPerDay")]
    public async Task<IActionResult> GetAverageBookingsPerDay()
    {
        var result = await _statisticsService.GetAverageBookingsPerDayAsync();

        return Ok(result);
    }

    [HttpGet("UserCount")]
    public async Task<IActionResult> GetUserCount()
    {
        var result = await _statisticsService.GetUserCountAsync();

        return Ok(result);
    }

    [HttpGet("FinishedBookingsCount")]
    public async Task<IActionResult> GetFinishedBookingsCount()
    {
        var result = await _statisticsService.GetFinishedBookingsCountAsync();

        return Ok(result);
    }
}