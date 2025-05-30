using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Evently.Modules.Events.Infrastructure.Database;

public class EventsDbContextFactory : IDesignTimeDbContextFactory<EventsDbContext>
{
    public EventsDbContext CreateDbContext(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<EventsDbContext>();
        string connectionString = configuration.GetConnectionString("Database")!;
        
        optionsBuilder
            .UseNpgsql(connectionString, npgsqlOptions =>
                npgsqlOptions.MigrationsHistoryTable(
                    Microsoft.EntityFrameworkCore.Migrations.HistoryRepository.DefaultTableName,
                    Schemas.Events))
            .UseSnakeCaseNamingConvention();

        return new EventsDbContext(optionsBuilder.Options);
    }
}
