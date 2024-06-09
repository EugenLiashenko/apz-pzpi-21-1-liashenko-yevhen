using Core.DTOs.Booking;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace BLL.Statistics;

public class StatisticsService: IStatisticsService
{
    private readonly DataContext _dataContext;

    public StatisticsService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<List<DayBookingsDto>> GetBookingsStartCountPerDayLastMonthAsync()
    {
        var monthStartDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(-1);
        var bookingsCountPerDay = await _dataContext.Bookings
            .Where(s => s.StartingDate >= monthStartDate)
            .GroupBy(s => s.StartingDate)
            .Select(group => new DayBookingsDto
            {
                Date = group.Key,
                BookingCount = group.Count()
            })
            .OrderBy(model => model.Date)
            .ToListAsync();

        return bookingsCountPerDay;
    }

    public async Task<List<DayBookingsDto>> GetBookingsEndCountPerDayLastMonthAsync()
    {
        var lastMonthStartDate = DateOnly.FromDateTime(DateTime.Now).AddMonths(-1);
        var bookingsCountPerDay = await _dataContext.Bookings
            .Where(s => s.EndingDate >= lastMonthStartDate)
            .GroupBy(s => s.EndingDate)
            .Select(group => new DayBookingsDto
            {
                Date = group.Key,
                BookingCount = group.Count()
            })
            .OrderBy(dto => dto.Date)
            .ToListAsync();

        return bookingsCountPerDay;
    }

    public async Task<int> GetBookingsLastWeekAsync()
    {
        var lastWeekStartDate = DateOnly.FromDateTime(DateTime.Now).AddDays(-7);
        var bookingCount = await _dataContext.Bookings.CountAsync(s => s.StartingDate >= lastWeekStartDate);

        return bookingCount;
    }

    public async Task<double> GetAverageBookingsPerDayAsync()
    {
        var startingDate = await _dataContext.Bookings.MinAsync(s => s.StartingDate);
        var endingDate = DateOnly.FromDateTime(DateTime.Now);
        var totalDays = (endingDate.DayNumber - startingDate.DayNumber);

        if (totalDays <= 0)
        {
            return -1;
        }
            
        var averageShipmentsPerDay = await _dataContext.Bookings.CountAsync() / totalDays;

        return averageShipmentsPerDay;
    }

    public async Task<int> GetUserCountAsync()
    {
        var users = await _dataContext.Users.CountAsync();

        return users;
    }

    public async Task<int> GetFinishedBookingsCountAsync()
    {
        var currentDate = DateOnly.FromDateTime(DateTime.Now);
        var finishedBookings = await _dataContext.Bookings.Select(b => b.EndingDate < currentDate)
            .ToListAsync();

        return finishedBookings.Count;
    }
}