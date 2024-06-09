using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Booking> Bookings { get; set; }

    public DbSet<Apartments> ApartmentsList { get; set; }

    public DbSet<Notification> Notifications { get; set; }

    public DbSet<ApartmentsCondition> ApartmentsConditions { get; set; }

    public DbSet<Measurement> Measurements { get; set; }
}