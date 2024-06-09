using Core.DTOs;
using Core.DTOs.Error;
using Core.DTOs.Measurements;
using Core.Entities;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace BLL.Measurements;

public class MeasurementService: IMeasurementService
{
    private readonly DataContext _dataContext;

    public MeasurementService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<IModel> CreateMeasurementAsync(int apartmentsId, MeasurementDto measurementDto)
    {
        var apartment = await _dataContext.ApartmentsList.FirstOrDefaultAsync(s => s.Id == apartmentsId);

        if (apartment is null)
        {
            return new ErrorDto()
            {
                Message = $"Apartments with id: {apartmentsId} was not found."
            };
        }

        var measurement = new Measurement()
        {
            Metric = measurementDto.Metrics,
            Value = measurementDto.Value,
            Timestamp = measurementDto.TimeSpan,
            Apartment = apartment
        };
        _dataContext.Measurements.Add(measurement);
        await _dataContext.SaveChangesAsync();

        return measurementDto;
        
    }

    public async Task<IModel> DeleteMeasurementAsync(int analysisId)
    {
        var analysis = await _dataContext.Measurements.FirstOrDefaultAsync(a => a.Id == analysisId);

        if (analysis is null) 
        {
            return new ErrorDto()
            {
                Message = $"Analysis with id: {analysisId} was not found."
            };
        }

        _dataContext.Measurements.Remove(analysis);
        await _dataContext.SaveChangesAsync();

        return new SuccessDto()
        {
            Message = "Measurement was successfully deleted."
        };
    }

    public async Task<List<MeasurementDto>> GetAllMeasurementsAsync()
    {
        var analysises = await _dataContext.Measurements.ToListAsync();

        return analysises.Select(a => new MeasurementDto
        {
            Metrics = a.Metric,
            Value  = a.Value,
            TimeSpan = a.Timestamp,
        }).ToList();
    }

    public async Task<IModel> GetMeasurementByIdAsync(int analysisId)
    {
        var analysis = await _dataContext.Measurements.FirstOrDefaultAsync(a => a.Id == analysisId);

        if (analysis is null)
        {
            return new ErrorDto
            {
                Message = $"No analysises with id: {analysisId} was not found."
            };
        }

        return new MeasurementDto() 
        { 
            Metrics = analysis.Metric,
            Value = analysis.Value,
            TimeSpan = analysis.Timestamp
        };
    }

    public async Task<IModel> UpdateMeasurementAsync(int measurementId, MeasurementDto measurementDto)
    {
        var measurement = await _dataContext.Measurements.FirstOrDefaultAsync(a => a.Id == measurementId);

        if (measurement is null)
        {
            return new ErrorDto
            {
                Message = $"Measurement with such id: {measurementId} was not found."
            };
        }

        measurement.Metric = measurementDto.Metrics;
        measurement.Value = measurementDto.Value;
        measurement.Timestamp = measurementDto.TimeSpan;

        await _dataContext.SaveChangesAsync();

        return measurementDto;
    }
}