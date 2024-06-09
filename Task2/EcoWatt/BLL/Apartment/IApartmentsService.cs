using Core.DTOs;
using Core.DTOs.Apartment;
using Core.Entities;
using Core.Enums;

namespace BLL.Apartment;

public interface IApartmentsService
{
    Task<List<Apartments>> GetAllApartmentsAsync();

    Task<Apartments?> GetApartmentsByIdAsync(int id);

    Task<Apartments> CreateApartmentsAsync(ApartmentCreateOrUpdateDto apartments);

    Task<IModel> UpdateApartmentsAsync(int id, ApartmentCreateOrUpdateDto apartments);

    Task<IModel> DeleteApartmentsAsync(int id);

    Task<List<Apartments>> GetApartmentsByStatusAsync(Availability status);
}