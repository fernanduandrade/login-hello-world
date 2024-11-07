using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AwesomeApp.API.Infra.External;

public interface IDiscordService
{
    Task<DiscordResponseDto> GetUserData(string accessToken);
}

public class DiscordService :IDiscordService
{
    private readonly HttpClient _httpClient;

    public DiscordService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
    }
    public async Task<DiscordResponseDto> GetUserData(string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        
        var response = await _httpClient.GetAsync($"");

        if (!response.IsSuccessStatusCode)
            return null;
        
        string json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<DiscordResponseDto>(json);
    }
}

public sealed record DiscordResponseDto(
    [property: JsonPropertyName("username")] string UserName,
    [property: JsonPropertyName("email")] string Email,
    [property: JsonPropertyName("global_name")] string GlobalName);