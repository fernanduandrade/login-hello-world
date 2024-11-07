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
        string url = $"https://github.com/login/oauth/access_token?client_secret=0175aa8a03d269447f7c8df0326b9f51e0df6d31&client_id=Ov23liwDSsytRcy0KgYy&code={code}&redirect_uri=http://localhost:5173/auth/callback?provider=github";
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
        string url = "https://api.github.com/user";
        
        _httpClient.DefaultRequestHeaders.Authorization 
            = new AuthenticationHeaderValue("Bearer",  accessToken);
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "Login-Hellow_World");
        
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