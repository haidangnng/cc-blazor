using QuestionLairClient.Models.Auth;
using System.Net.Http.Json;

namespace QuestionLairClient.Services.Auth;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly JwtAuthenticationStateProvider _authProvider;

    public AuthService(HttpClient httpClient, JwtAuthenticationStateProvider authProvider)
    {
        _httpClient = httpClient;
        _authProvider = authProvider;
    }

    public async Task<bool> LoginAsync(string email, string password)
    {
        var response = await _httpClient.PostAsJsonAsync("/auth/login", new { email, password });

        if (!response.IsSuccessStatusCode) return false;

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        if (result?.Token is null) return false;

        _authProvider.MarkUserAsAuthenticated(result.Token);
        return true;
    }

    public void Logout()
    {
        _authProvider.MarkUserAsLoggedOut();
    }

    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}

