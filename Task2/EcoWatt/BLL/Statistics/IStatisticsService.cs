using Core.DTOs.Booking;

namespace BLL.Statistics;

public interface IStatisticsService
{
    Task<List<DayBookingsDto>> GetBookingsStartCountPerDayLastMonthAsync();

    Task<List<DayBookingsDto>> GetBookingsEndCountPerDayLastMonthAsync();

    Task<int> GetBookingsLastWeekAsync();

    Task<double> GetAverageBookingsPerDayAsync();

    Task<int> GetUserCountAsync();

    Task<int> GetFinishedBookingsCountAsync();
}