using System.Linq.Expressions;
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

    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate = null, bool readOnly = false)
    {
        var query = _dbSet.AsQueryable();
        
        if(predicate is not null)
            query = query.Where(predicate);
        
        if(readOnly)
            query = query.AsNoTracking();
        
        return query;
    }
}