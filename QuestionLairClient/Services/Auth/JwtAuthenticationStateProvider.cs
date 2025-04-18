using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using QuestionLairClient.Models.Auth;

namespace QuestionLairClient.Services.Auth;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ITokenStorage _tokenStorage;

    public JwtAuthenticationStateProvider(ITokenStorage tokenStorage)
    {
        _tokenStorage = tokenStorage;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        if (!string.IsNullOrEmpty(_tokenStorage.Token))
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(_tokenStorage.Token);
            identity = new ClaimsIdentity(token.Claims, "jwt");
        }

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }

    public void MarkUserAsAuthenticated(string token)
    {
        _tokenStorage.Token = token;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void MarkUserAsLoggedOut()
    {
        _tokenStorage.Token = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}

