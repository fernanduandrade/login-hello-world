using AwesomeApp.API.Contracts;
using AwesomeApp.API.Infra.Data;
using AwesomeApp.API.Infra.Data.Repositories;
using AwesomeApp.API.Infra.External;
using AwesomeApp.API.Shared;
using Microsoft.EntityFrameworkCore;

namespace AwesomeApp.API.Handlers;

public class GoogleHandler(IGoogleService googleService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ILoginProviderHandler
{
    public async Task<Result<User, Error>> HandleLoginAsync(string code)
    {
        var googleUser = await googleService.GetUser(code);
        
        if (googleUser is null)
            return new Error("User.Error", "Failed to retrive user information");
        
        var userExists = await userRepository
            .Get(x => x.Email == googleUser.Email, true)
            .FirstOrDefaultAsync();

        if (userExists is not null)
            return userExists;
        
        userRepository.Add(googleUser);
        await unitOfWork.Commit();

        return googleUser;
    }
}