using AwesomeApp.API.Handlers;
using AwesomeApp.API.Infra.Data;
using AwesomeApp.API.Infra.Data.Repositories;
using AwesomeApp.API.Infra.External;
using AwesomeApp.API.Shared;

namespace AwesomeApp.API.Setup;

public static class DependecyInjection
{
    public static void InjectDependencies(this IServiceCollection services)
    {
        // Persistence
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Factories
        services.AddScoped<LoginProviderFactory>();
        
        // handlers
        services.AddScoped<ILoginProviderHandler, DiscordHandler>();
        services.AddScoped<ILoginProviderHandler, GithubHandle>();
        services.AddScoped<ILoginProviderHandler, GoogleHandler>();

        // external
        services.AddScoped<IGoogleService, GoogleService>();
        services.AddScoped<IGithubService, GithubService>();
        services.AddHttpClient<IDiscordService, DiscordService>(opt =>
            {
                opt.BaseAddress = new Uri(Environment.GetEnvironmentVariable("DISCORD_API")!);
            })
            .AddPolicyHandler(Resiliency.GetRetryPolicy())
            .AddPolicyHandler(Resiliency.GetCircuitBreakerPolicy());

    }
}