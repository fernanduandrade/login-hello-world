using AwesomeApp.API.Infra.Data;
using AwesomeApp.API.Setup;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = builder.Configuration.GetConnectionString("DbConnection")!;
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

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("AuthDiscord")
.WithOpenApi();

app.Run();
