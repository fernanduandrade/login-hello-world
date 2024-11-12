using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using AwesomeApp.API.Contracts;

namespace AwesomeApp.API.Infra.External;

public interface IGithubService
{
    Task<string> GetAccessToken(string code);
    Task<User> GetUser(string accessToken);
}

public class GithubService : IGithubService
{
    private readonly HttpClient _httpClient;
    
    public GithubService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<string> GetAccessToken(string code)
    {
        string url = Environment.GetEnvironmentVariable("GITHUB_OAUTH2_TOKEN")!;
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        var response = await _httpClient.GetAsync(url);

        if (response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GithubAccessTokenResponse>(content).AccessToken;
        }

        return null;
    }

    public async Task<User> GetUser(string code)
    {
        var accessToken = await GetAccessToken(code);
        string url = $"{Environment.GetEnvironmentVariable("GITHUB_API")!}/user";
        
        _httpClient.DefaultRequestHeaders.Authorization 
            = new AuthenticationHeaderValue("Bearer",  accessToken);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", Environment.GetEnvironmentVariable("GITHUB_AGENT")!);
        
        var response = await _httpClient.GetAsync(url);
        string content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<GithubUserResponse>(content);

            return User.Create(result.UserName, result.Email);
        }
        
        return null;
    }
}


public sealed record GithubAccessTokenResponse(
    [property: JsonPropertyName("access_token")] string AccessToken);
    
public sealed record GithubUserResponse(
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("login")] string UserName);