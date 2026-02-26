using Microsoft.Extensions.DependencyInjection;
using StoreWebsite.Application.Interfaces;
using StoreWebsite.Application.Services;

namespace StoreWebsite.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductUseCaseService, ProductUseCaseService>();
        return services;
    }
}
