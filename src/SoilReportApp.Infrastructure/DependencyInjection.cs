using Microsoft.Extensions.DependencyInjection;
using SoilReportApp.Domain.Interfaces.Repositories;
using SoilReportApp.Infrastructure.Repositories;

namespace SoilReportApp.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Registers all infrastructure layer services (repositories) with the dependency injection container.
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Register repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRequestRepository, RequestRepository>();
        services.AddScoped<ILookupRepository, LookupRepository>();
        
        return services;
    }
}
