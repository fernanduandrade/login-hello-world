using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using AwesomeApp.API.Contracts;
using AwesomeApp.API.Shared;

namespace AwesomeApp.API.Infra.External;

public interface IGoogleService
{
    Task<User> GetUser(string accessToken);
}

public class GoogleService() : IGoogleService
{
    private async Task<string> GetAccessToken(string code)
    {
        var url = new Uri(Environment.GetEnvironmentVariable("GOOGLE_OAUTH2_TOKEN")!)
            .AddParameter("code", code)
            .AddParameter("grant_type", "authorization_code")
            .AddParameter("redirect_uri", Environment.GetEnvironmentVariable("GOOGLE_DNS_REDIRECT")!)
            .AddParameter("client_secret", Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET")!)
            .AddParameter("client_id", Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID")!);

        var client = new HttpClient() { BaseAddress = url};
        var response = await client.PostAsync("", null);
        if (!response.IsSuccessStatusCode)
            return null;
        
        var result = await response.Content.ReadAsStringAsync();
        var data = JsonSerializer.Deserialize<GoogleTokenResponse>(result);
        return data.AccessToken;
    }

    public async Task<User> GetUser(string code)
    {
        var bearer = await GetAccessToken(code);
        var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);
        var response = await client.GetAsync(Environment.GetEnvironmentVariable("GOOGLE_API")!);
        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return null;

        var result = JsonSerializer.Deserialize<GoogleUserInfoResponse>(responseString);
        
        return User.Create(result.UserName, result.Email);
    }
}

public sealed record GoogleTokenResponse(
    [property: JsonPropertyName("access_token")] string AccessToken,
    [property: JsonPropertyName("id_token")] string IdToken
    );
    
public sealed record GoogleUserInfoResponse(
    [property: JsonPropertyName("name")] string UserName,
    [property: JsonPropertyName("email")] string Email
);