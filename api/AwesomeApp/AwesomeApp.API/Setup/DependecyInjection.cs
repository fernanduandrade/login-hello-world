using AwesomeApp.API.Infra.Data;
using AwesomeApp.API.Infra.Data.Repositories;

namespace AwesomeApp.API.Setup;

public static class DependecyInjection
{
    public static void InjectDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
    }
}