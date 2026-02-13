using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SoilReportApp.Application.Interfaces;
using SoilReportApp.Application.Services;
using SoilReportApp.Infrastructure;

namespace SoilReportApp.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Registers all application layer services with the dependency injection container.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructureServices(configuration);
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRequestService, RequestService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
