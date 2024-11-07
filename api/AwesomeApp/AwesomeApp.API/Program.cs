using System.Text.Json;
using System.Text.Json.Serialization;
using AwesomeApp.API.Handlers;
using AwesomeApp.API.Infra.Data;
using AwesomeApp.API.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, true));
});

string connection = builder.Configuration.GetConnectionString("DbConnection")!;
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection, opt =>
{
    opt.EnableRetryOnFailure(3, TimeSpan.FromSeconds(10), null);
}));

builder.Services.InjectDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/api/users/auth", async ([FromServices] IDiscordHandler discordHandler, [FromServices] IGithubHandler githubHandler, [FromServices] IGoogleHandler googleHandler ,LoginDto request) =>
{
    if (request.LoginProvider == LoginProvider.Discord)
    {
        var user = await discordHandler.HandleLoginAsync(request.Token);
        if (user is not null)
            return Results.Ok(user);
        
        return Results.Unauthorized();
    }
    
    if (request.LoginProvider == LoginProvider.Github)
    {
        var user = await githubHandler.HandleLoginAsync(request.Token);
        if (user is not null)
            return Results.Ok(user);
        
        return Results.Unauthorized();
    }

    if (request.LoginProvider == LoginProvider.Google)
    {
        var user = await googleHandler.HandleLoginAsync(request.Token);
        if (user is not null)
            return Results.Ok(user);
        
        return Results.Unauthorized();
    }
    return Results.Unauthorized();
})
.WithName("Auth")
.WithOpenApi();

app.UseCors(cp => cp
    .AllowAnyOrigin()
    .SetPreflightMaxAge(TimeSpan.FromHours(24))
    .AllowAnyMethod()
    .AllowAnyHeader());

app.Run();


public enum LoginProvider {
    Google,
    Discord,
    Github
}

public sealed record LoginDto(
    [property: JsonPropertyName("provider")] LoginProvider LoginProvider,
    [property: JsonPropertyName("token")] string Token);