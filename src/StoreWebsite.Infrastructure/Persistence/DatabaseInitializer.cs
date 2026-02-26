using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StoreWebsite.Domain.Entities;

namespace StoreWebsite.Infrastructure.Persistence;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseInitializer");

        try
        {
            await dbContext.Database.MigrateAsync(cancellationToken);

            if (!await dbContext.Products.AnyAsync(cancellationToken))
            {
                var seedProducts = new[]
                {
                    Product.Create("Mechanical Keyboard", "RGB compact keyboard", 85.00m, 20),
                    Product.Create("Gaming Mouse", "Ergonomic wireless mouse", 45.50m, 35),
                    Product.Create("4K Monitor", "27-inch IPS monitor", 320.00m, 10)
                };

                await dbContext.Products.AddRangeAsync(seedProducts, cancellationToken);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "An error occurred while initializing the database.");
            throw;
        }
    }
}
