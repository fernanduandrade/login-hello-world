using AwesomeApp.API.Contracts;
using AwesomeApp.API.Infra.Data;
using AwesomeApp.API.Infra.Data.Repositories;
using AwesomeApp.API.Infra.External;
using Microsoft.EntityFrameworkCore;

namespace AwesomeApp.API.Handlers;

public class DiscordHandler : ILoginProviderHandler
{
    private readonly IDiscordService _discordService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DiscordHandler(IDiscordService discordService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _discordService = discordService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<User> HandleLoginAsync(string token)
    {
        var discordResponse = await _discordService.GetUserData(token);
        if (discordResponse is null)
            return null;

        var userExists = await _userRepository
            .Get(x => x.Email == discordResponse.Email, true)
            .FirstOrDefaultAsync();

        if (userExists is not null)
            return userExists;

        var user = User.Create(discordResponse.UserName, discordResponse.Email);
        _userRepository.Add(user);
        await _unitOfWork.Commit();

        return user;
    }
}

public class GithubHandle : ILoginProviderHandler
{
    private readonly IGithubService _githubService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GithubHandle(IGithubService githubService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _githubService = githubService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<User> HandleLoginAsync(string code)
    {
        var githubResponse = await _githubService.GetUser(code);
        
        if (githubResponse is null)
            return null;

        var userExists = await _userRepository
            .Get(x => x.Email == githubResponse.Email, true)
            .FirstOrDefaultAsync();

        if (userExists is not null)
            return userExists;

        var user = User.Create(githubResponse.UserName, githubResponse.Email);
        _userRepository.Add(user);
        await _unitOfWork.Commit();

        return user;
    }
}


public class GoogleHandler : ILoginProviderHandler
{
    private readonly IGoogleService _googleService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GoogleHandler(IGoogleService googleService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _googleService = googleService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<User> HandleLoginAsync(string code)
    {
        var googleResponse = await _googleService.GetUser(code);
        
        if (googleResponse is null)
            return null;

        var userExists = await _userRepository
            .Get(x => x.Email == googleResponse.Email, true)
            .FirstOrDefaultAsync();

        if (userExists is not null)
            return userExists;

        var user = User.Create(googleResponse.UserName, googleResponse.Email);
        _userRepository.Add(user);
        await _unitOfWork.Commit();

        return user;
    }
}