using AwesomeApp.API.Handlers;
using AwesomeApp.API.Infra.Data;
using AwesomeApp.API.Infra.Data.Repositories;
using AwesomeApp.API.Infra.External;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;

namespace AwesomeApp.API.Setup;

public static class DependecyInjection
{
    public static void InjectDependencies(this IServiceCollection services)
    {
        var policyRetry = 
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IDiscordHandler, DiscordHandler>();
        
        services.AddHttpClient<IDiscordService, DiscordService>(opt =>
            {
                opt.BaseAddress = new Uri("https://discord.com/api/users/@me");
            })
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());

        services.AddScoped<IGithubService, GithubService>();
        services.AddScoped<IGithubHandler, GithubHandle>();
        services.AddScoped<IGoogleHandler, GoogleHandler>();
        services.AddScoped<IGoogleService, GoogleService>();

    }
    
    static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,
                retryAttempt)));
    }
    
    static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}