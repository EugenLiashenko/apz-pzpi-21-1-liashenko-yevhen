using BLL.Measurements;
using Core.DTOs.Error;
using Core.DTOs.Measurements;
using EcoWattServer.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoWattServer.Controllers;

[Authorize]
[AdminAuth]
[ApiController]
[Route("Api/[controller]")]
public class MeasurementController : Controller
{
    private readonly IMeasurementService _measurementService;

    public MeasurementController(IMeasurementService measurementService)
    {
        _measurementService = measurementService;
    }
    
    [HttpGet("{measurementId}")]
    public async Task<IActionResult> GetAnalysisById(int measurementId)
    {
        var result = await _measurementService.GetMeasurementByIdAsync(measurementId);

        if (result is ErrorDto)
        {
            var errorResult = result as ErrorDto;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAnalysis()
    {
        var result = await _measurementService.GetAllMeasurementsAsync();
        
        //Added test comment
        return Ok(result);
    }

    [HttpPost("{apartmentId}")]
    public async Task<IActionResult> CreateAnalysis(int apartmentId, [FromBody] MeasurementDto analysisModel)
    {
        var result = await _measurementService.CreateMeasurementAsync(apartmentId, analysisModel);

        if (result is ErrorDto)
        {
            var errorResult = result as ErrorDto;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpPut("{measurementId}")]
    public async Task<IActionResult> UpdateAnalysis(int measurementId, [FromBody] MeasurementDto analysisModel)
    {
        var result = await _measurementService.UpdateMeasurementAsync(measurementId, analysisModel);

        if (result is ErrorDto)
        {
            var errorResult = result as ErrorDto;
            
            return NotFound(errorResult.Message);
        }

        return Ok(result);
    }

    [HttpDelete("{measurementId}")]
    public async Task<IActionResult> DeleteAnalysis(int measurementId)
    {
        var result = await _measurementService.DeleteMeasurementAsync(measurementId);

        if (result is SuccessDto)
        {
            return NoContent();
        }
        
        var errorResult = result as ErrorDto;
            
        return BadRequest(errorResult.Message);
    }
}