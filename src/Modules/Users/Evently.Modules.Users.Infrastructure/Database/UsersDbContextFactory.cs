using Evently.Modules.Users.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
namespace Evently.Modules.Users.Infrastructure.Database;

public class UsersDbContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
{
    public UsersDbContext CreateDbContext(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();
        string connectionString = configuration.GetConnectionString("Database")!;
        
        optionsBuilder
            .UseNpgsql(connectionString, npgsqlOptions =>
                npgsqlOptions.MigrationsHistoryTable(
                    Microsoft.EntityFrameworkCore.Migrations.HistoryRepository.DefaultTableName,
                    Schemas.Users))
            .UseSnakeCaseNamingConvention();

        return new UsersDbContext(optionsBuilder.Options);
    }
}
