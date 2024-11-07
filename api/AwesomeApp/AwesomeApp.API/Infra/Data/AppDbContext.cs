using AwesomeApp.API.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AwesomeApp.API.Infra.Data;

public class AppDbContext : DbContext
{
    DbSet<User> Users { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
}