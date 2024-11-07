using AwesomeApp.API.Contracts;

namespace AwesomeApp.API.Handlers;

public interface ILoginProviderHandler
{
    Task<User> HandleLoginAsync(string token);
}