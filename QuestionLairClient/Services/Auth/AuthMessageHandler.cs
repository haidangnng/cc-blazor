using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using QuestionLairClient.Models.Auth;

namespace QuestionLairClient.Services.Auth;

public class AuthMessageHandler : DelegatingHandler
{
    private readonly ITokenStorage _tokenStorage;
    private readonly NavigationManager _navigation;

    public AuthMessageHandler(ITokenStorage tokenStorage, NavigationManager navigation)
    {
        _tokenStorage = tokenStorage;
        _navigation = navigation;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _tokenStorage.Token;

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // If unauthorized, redirect to login
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            _navigation.NavigateTo("/login", forceLoad: true);
        }

        return response;
    }
}

