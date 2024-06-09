using Core.DTOs;
using Core.DTOs.Apartment;
using Core.DTOs.Error;
using Core.Entities;
using Core.Enums;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace BLL.Apartment;

public class ApartmentsService: IApartmentsService
{
    private readonly DataContext _dataContext;

    public ApartmentsService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }
    
    public async Task<List<Apartments>> GetAllApartmentsAsync()
    {
        return await _dataContext.ApartmentsList.ToListAsync();
    }

    public async Task<Apartments?> GetApartmentsByIdAsync(int id)
    {
        var apartments = await _dataContext.ApartmentsList.FirstOrDefaultAsync(a => a.Id == id);

        if (apartments is null)
        {
            return null;
        }

        return apartments;
    }

    public async Task<Apartments> CreateApartmentsAsync(ApartmentCreateOrUpdateDto apartments)
    {
        var dbApartments = new Apartments()
        {
            Capacity = apartments.Capacity,
            Condition = apartments.Condition,
            Status = apartments.Status,
            Type = apartments.Type,
            StartingPrice = apartments.StartingPrice,
        };
        
        _dataContext.ApartmentsList.Add(dbApartments);
        await _dataContext.SaveChangesAsync();

        return dbApartments;
    }

    public async Task<IModel> UpdateApartmentsAsync(int id, ApartmentCreateOrUpdateDto apartments)
    {
        var dbApartments = await _dataContext.ApartmentsList.FirstOrDefaultAsync(a => a.Id == id);

        if (dbApartments is null)
        {
            return new ErrorDto()
            {
                Message = $"Apartments with id: {id} was not found",
            };
        }

        dbApartments.Capacity = apartments.Capacity;
        dbApartments.StartingPrice = apartments.StartingPrice;
        dbApartments.Condition = apartments.Condition;
        dbApartments.Status = apartments.Status;
        dbApartments.Type = apartments.Type;

        _dataContext.ApartmentsList.Update(dbApartments);
        await _dataContext.SaveChangesAsync();

        return apartments;
    }

    public async Task<IModel> DeleteApartmentsAsync(int id)
    {
        var apartments = await _dataContext.ApartmentsList.FirstOrDefaultAsync(aa => aa.Id == id);

        if (apartments is null)
        {
            return new ErrorDto()
            {
                Message = $"Apartments with id: {id} was not found",
            };
        }
        
        _dataContext.ApartmentsList.Remove(apartments);
        await _dataContext.SaveChangesAsync();

        return new SuccessDto()
        {
            Message = "Apartments was successfully deleted."
        };
    }

    public async Task<List<Apartments>> GetApartmentsByStatusAsync(Availability status)
    {
        var apartments = await _dataContext.ApartmentsList
            .Where(a => a.Status == status)
            .ToListAsync();

        return apartments;
    }
}