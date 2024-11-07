// using AwesomeApp.API.Contracts;
//
// namespace AwesomeApp.API.Services;
//
// public interface IAuthService
// {
//     Task<User> Authenticate(LoginProvider loginProvider, string token);
// }
//
// public class AuthService : IAuthService
// {
//     private readonly ILoginProviderHandler _loginHandler;
//     
//     public async Task<User> Authenticate(LoginProvider loginProvider, string token)
//     {
//         var handler = _loginProviderFactory.GetHandler(provider);
//         return handler.HandleLoginAsync(token);
//     }
// }