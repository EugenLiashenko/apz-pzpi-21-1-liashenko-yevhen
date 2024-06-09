using Core.DTOs;
using Core.DTOs.Booking;
using Core.Entities;

namespace BLL.Bookings;

public interface IBookingService
{
    public Task<Booking?> GetBookingById(int id);

    public Task<List<Booking>> GetAllBookings();

    public Task<IModel> DeleteBooking(int id);

    public Task<Booking> CreateBooking(BookingCreateOrUpdateDto booking);

    public Task<IModel> UpdateBooking(int id, BookingCreateOrUpdateDto booking);
}