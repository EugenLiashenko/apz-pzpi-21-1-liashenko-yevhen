using BLL.Apartment;
using Core.DTOs.Apartment;
using Core.DTOs.Error;
using Core.Enums;
using EcoWattServer.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoWattServer.Controllers;

[Authorize]
[AdminAuth]
[ApiController]
[Route("Api/[controller]")]
public class ApartmentsController : Controller
{
    private readonly IApartmentsService _apartmentsService;

    public ApartmentsController(IApartmentsService apartmentsService)
    {
        _apartmentsService = apartmentsService;
    }
    
    [HttpGet("{apartmentsId}")]
    public async Task<IActionResult> GetApartmentsById(int apartmentsId)
    {
        var result = await _apartmentsService.GetApartmentsByIdAsync(apartmentsId);

        if (result is null)
        {
            return NotFound($"Apartments with id: {apartmentsId} was not found.");
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllApartments()
    {
        var result = await _apartmentsService.GetAllApartmentsAsync();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateApartments([FromBody] ApartmentCreateOrUpdateDto apartmentsModel)
    {
        var result = await _apartmentsService.CreateApartmentsAsync(apartmentsModel);
        
        return Ok(result);
    }

    [HttpPut("{apartmentsId}")]
    public async Task<IActionResult> UpdateApartments(int apartmentsId, [FromBody] ApartmentCreateOrUpdateDto apartmentsModel)
    {
        var result = await _apartmentsService.UpdateApartmentsAsync(apartmentsId, apartmentsModel);

        if (result is ErrorDto)
        {
            var errorResult = result as ErrorDto;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpDelete("{apartmentsId}")]
    public async Task<IActionResult> DeleteApartments(int apartmentsId)
    {
        var result = await _apartmentsService.DeleteApartmentsAsync(apartmentsId);

        if (result is SuccessDto)
        {
            return NoContent();
        }
        
        var errorResult = result as ErrorDto;
            
        return BadRequest(errorResult.Message);
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetApartmentsByStatus(Availability status)
    {
        var apartments = await _apartmentsService.GetApartmentsByStatusAsync(status);

        return Ok(apartments);
    }
}