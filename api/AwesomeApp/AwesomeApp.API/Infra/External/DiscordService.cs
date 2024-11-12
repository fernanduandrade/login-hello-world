using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using AwesomeApp.API.Contracts;

namespace AwesomeApp.API.Infra.External;

public interface IDiscordService
{
    Task<User> GetUser(string accessToken);
}

public class DiscordService :IDiscordService
{
    private readonly HttpClient _httpClient;

    public DiscordService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
    }
    public async Task<User> GetUser(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        
        var response = await _httpClient.GetAsync($"");
        if (!response.IsSuccessStatusCode)
            return null;
        
        string jsonString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<DiscordResponseDto>(jsonString);
        return User.Create(result.UserName, result.Email);
    }
}

public sealed record DiscordResponseDto(
    [property: JsonPropertyName("username")] string UserName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("global_name")] string GlobalName);