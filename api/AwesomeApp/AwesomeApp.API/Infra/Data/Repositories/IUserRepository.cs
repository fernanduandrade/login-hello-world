using AwesomeApp.API.Contracts;

namespace AwesomeApp.API.Infra.Data.Repositories;

public interface IUserRepository
{
    void Add(User user);
}