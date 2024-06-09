using Core.DTOs;
using Core.DTOs.Booking;
using Core.DTOs.Error;
using Core.Entities;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace BLL.Bookings;

public class BookingService: IBookingService
{
    private readonly DataContext _dataContext;

    public BookingService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<Booking?> GetBookingById(int id)
    {
        var booking = await _dataContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);

        if (booking is null)
        {
            return null;
        }

        return booking;
    }

    public async Task<List<Booking>> GetAllBookings()
    {
        return await _dataContext.Bookings.ToListAsync();
    }

    public async Task<IModel> DeleteBooking(int id)
    {
        var booking = await _dataContext.Bookings.FirstOrDefaultAsync(b => b.Id == id);

        if (booking is null)
        {
            return new ErrorDto()
            {
                Message = $"Booking with id: {id} was not found.",
            };
        }

        _dataContext.Bookings.Remove(booking);
        await _dataContext.SaveChangesAsync();

        return new SuccessDto()
        {
            Message = "Booking was successfully deleted."
        };
    }

    public async Task<Booking> CreateBooking(BookingCreateOrUpdateDto booking)
    {
        var dbBooking = new Booking
        {
            ApartmentId = booking.ApartmentId,
            UserId = booking.UserId,
            EndingDate = booking.EndingDate,
            StartingDate = booking.StartingDate,
            TotalPrice = booking.TotalPrice,
            PriceForManagement = booking.PriceForManagement
        };

        var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == dbBooking.UserId);

        if (user.Bookings is null)
        {
            user.Bookings = new List<Booking>()
            {
                dbBooking,
            };
        }
        
        user.Bookings.Add(dbBooking);
        
        _dataContext.Users.Update(user);
        _dataContext.Bookings.Add(dbBooking);
        await _dataContext.SaveChangesAsync();

        return dbBooking;
    }

    public async Task<IModel> UpdateBooking(int id, BookingCreateOrUpdateDto booking)
    {
        var dbBooking = await _dataContext.Bookings.FirstOrDefaultAsync(a => a.Id == id);

        if (dbBooking is null)
        {
            return new ErrorDto()
            {
                Message = $"Booking with id: {id} was not found",
            };
        }

        dbBooking.ApartmentId = booking.ApartmentId;
        dbBooking.UserId = booking.UserId;
        dbBooking.EndingDate = booking.EndingDate;
        dbBooking.StartingDate = booking.StartingDate;
        dbBooking.TotalPrice = booking.TotalPrice;
        dbBooking.PriceForManagement = booking.PriceForManagement;

        _dataContext.Bookings.Update(dbBooking);
        await _dataContext.SaveChangesAsync();

        return booking;
    }
}