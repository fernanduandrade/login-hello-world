using AwesomeApp.API.Contracts;
using Microsoft.EntityFrameworkCore;

namespace AwesomeApp.API.Infra.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DbSet<User> _dbSet;

    public UserRepository(AppDbContext context)
    {
        _dbSet = context.Set<User>();
    }
    
    public void Add(User user)
        => _dbSet.Add(user);
}