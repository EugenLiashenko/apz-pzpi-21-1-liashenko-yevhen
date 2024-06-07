using Core.DTOs;
using Core.DTOs.Measurements;

namespace BLL.Measurements;

public interface IMeasurementService
{
    Task<IModel> GetMeasurementByIdAsync(int measurementId);

    Task<List<MeasurementDto>> GetAllMeasurementsAsync();

    Task<IModel> CreateMeasurementAsync(int apartmentsId, MeasurementDto measurementDto);

    Task<IModel> UpdateMeasurementAsync(int measurementId, MeasurementDto measurementDto);

    Task<IModel> DeleteMeasurementAsync(int measurementId);
}