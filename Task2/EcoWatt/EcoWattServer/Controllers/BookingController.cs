using BLL.Bookings;
using Core.DTOs.Booking;
using Core.DTOs.Error;
using EcoWattServer.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoWattServer.Controllers;

[Authorize]
[AdminAuth]
[ApiController]
[Route("Api/[controller]")]
public class BookingController : Controller
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    [HttpGet("{bookingId}")]
    public async Task<IActionResult> GetBookingById(int bookingId)
    {
        var result = await _bookingService.GetBookingById(bookingId);

        if (result is null)
        {
            return NotFound($"Bookings with id: {bookingId} was not found.");
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBookings()
    {
        var result = await _bookingService.GetAllBookings();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingCreateOrUpdateDto bookingModel)
    {
        var result = await _bookingService.CreateBooking(bookingModel);
        
        return Ok(result);
    }

    [HttpPut("{bookingId}")]
    public async Task<IActionResult> UpdateBooking(int bookingId, [FromBody] BookingCreateOrUpdateDto bookingModel)
    {
        var result = await _bookingService.UpdateBooking(bookingId, bookingModel);

        if (result is ErrorDto)
        {
            var errorResult = result as ErrorDto;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpDelete("{bookingId}")]
    public async Task<IActionResult> DeleteBooking(int bookingId)
    {
        var result = await _bookingService.DeleteBooking(bookingId);

        if (result is SuccessDto)
        {
            return NoContent();
        }
        
        var errorResult = result as ErrorDto;
            
        return BadRequest(errorResult.Message);
    }
}