namespace AwesomeApp.API.Handlers;

public class LoginProviderFactory
{
    private readonly Dictionary<LoginProvider, ILoginProviderHandler> _handlers;

    public LoginProviderFactory(IEnumerable<ILoginProviderHandler> handlers)
    {
        _handlers = handlers.ToDictionary(h => GetProviderEnum(h));
    }

    private LoginProvider GetProviderEnum(ILoginProviderHandler handler)
    {
        if (handler is GoogleHandler)
            return LoginProvider.Google;
        if (handler is GithubHandle)
            return LoginProvider.Github;
        if (handler is DiscordHandler)
            return LoginProvider.Discord;
        
        throw new ArgumentException("Handler não suportado");
    }

    public ILoginProviderHandler GetHandler(LoginProvider provider)
    {
        if (_handlers.TryGetValue(provider, out var handler))
            return handler;
        
        throw new ArgumentException("Provider não suportado");
    }
}