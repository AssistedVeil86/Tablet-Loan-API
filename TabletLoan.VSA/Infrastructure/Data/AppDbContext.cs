using Microsoft.EntityFrameworkCore;
using TabletLoan.VSA.Domain.Entities;
namespace TabletLoan.VSA.Infrastructure.Data;

public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    IConfiguration configuration) : DbContext(options)
{
    public DbSet<Loan> Loans { get; set; }
    public DbSet<Tablet> Tablets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasDefaultSchema("LoanSys");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        optionsBuilder.UseNpgsql(connectionString);

        var deviceId = configuration.GetValue<string>("Tablets:DeviceId")!;

        optionsBuilder
            .UseSeeding((context, _) =>
                {
                    var hasData = context.Set<Tablet>().Any();

                    if (!hasData)
                    {
                        context.Set<Tablet>().AddRange(
                            Tablet.Create("Tablet-1", "5", "21", deviceId,
                                configuration.GetValue<string>("Tablets:Tablet-1-CId")!),
                            Tablet.Create("Tablet-2", "5", "21", deviceId,
                                configuration.GetValue<string>("Tablets:Tablet-2-CId")!),
                            Tablet.Create("Tablet-3", "5", "21", deviceId,
                                configuration.GetValue<string>("Tablets:Tablet-3-CId")!),
                            Tablet.Create("Tablet-4", "5", "21", deviceId,
                                configuration.GetValue<string>("Tablets:Tablet-4-CId")!),
                            Tablet.Create("Tablet-5", "5", "21", deviceId,
                                configuration.GetValue<string>("Tablets:Tablet-5-CId")!),
                            Tablet.Create("Tablet-6", "5", "21", deviceId,
                                configuration.GetValue<string>("Tablets:Tablet-6-CId")!)
                        );

                        context.SaveChanges();
                    }
                });
    }
}