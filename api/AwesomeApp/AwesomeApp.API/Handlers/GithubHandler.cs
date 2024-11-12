using AwesomeApp.API.Contracts;
using AwesomeApp.API.Infra.Data;
using AwesomeApp.API.Infra.Data.Repositories;
using AwesomeApp.API.Infra.External;
using AwesomeApp.API.Shared;
using Microsoft.EntityFrameworkCore;

namespace AwesomeApp.API.Handlers;

public class GithubHandle(IGithubService githubService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ILoginProviderHandler
{
    public async Task<Result<User, Error>> HandleLoginAsync(string code)
    {
        var githubUser = await githubService.GetUser(code);
        
        if (githubUser is null)
            return new Error("User.Error", "Failed to retrive user information");

        var userExists = await userRepository
            .Get(x => x.Email == githubUser.Email, true)
            .FirstOrDefaultAsync();

        if (userExists is not null)
            return userExists;

        userRepository.Add(githubUser);
        await unitOfWork.Commit();

        return githubUser;
    }
}