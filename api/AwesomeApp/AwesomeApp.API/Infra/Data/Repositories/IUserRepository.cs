using System.Linq.Expressions;
using AwesomeApp.API.Contracts;

namespace AwesomeApp.API.Infra.Data.Repositories;

public interface IUserRepository
{
    void Add(User user);
    IQueryable<User> Get(Expression<Func<User, bool>>? predicate = null, bool readOnly = false);
}