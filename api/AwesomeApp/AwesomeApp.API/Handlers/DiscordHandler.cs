using AwesomeApp.API.Contracts;
using AwesomeApp.API.Infra.Data;
using AwesomeApp.API.Infra.Data.Repositories;
using AwesomeApp.API.Infra.External;
using AwesomeApp.API.Shared;
using Microsoft.EntityFrameworkCore;

namespace AwesomeApp.API.Handlers;

public class DiscordHandler(IDiscordService discordService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ILoginProviderHandler
{
    public async Task<Result<User, Error>> HandleLoginAsync(string token)
    {
        var discordUser = await discordService.GetUser(token);
        
        if (discordUser is null)
            return new Error("User.Error", "Failed to retrive user information");

        var userExists = await userRepository
            .Get(x => x.Email == discordUser.Email, true)
            .FirstOrDefaultAsync();

        if (userExists is not null)
            return userExists;

        userRepository.Add(discordUser);
        await unitOfWork.Commit();

        return discordUser;
    }
}