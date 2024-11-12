using AwesomeApp.API.Contracts;
using AwesomeApp.API.Shared;

namespace AwesomeApp.API.Handlers;

public interface ILoginProviderHandler
{
    Task<Result<User, Error>> HandleLoginAsync(string token);
}