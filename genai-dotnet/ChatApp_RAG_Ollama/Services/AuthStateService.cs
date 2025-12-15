using Microsoft.JSInterop;
using System.Text.Json;

namespace ChatApp_RAG_Ollama.Services;

public class AuthStateService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly HttpClient _httpClient;
    private bool _isAuthenticated;
    private string? _username;

    public AuthStateService(IJSRuntime jsRuntime, HttpClient httpClient)
    {
        _jsRuntime = jsRuntime;
        _httpClient = httpClient;
    }

    public bool IsAuthenticated => _isAuthenticated;
    public string? Username => _username;

    public async Task<bool> CheckAuthenticationAsync()
    {
        try
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            var username = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "username");
            
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(username))
            {
                _isAuthenticated = false;
                _username = null;
                return false;
            }

            // For now, just check if token exists (in production, you'd validate the JWT)
            _isAuthenticated = true;
            _username = username;
            return true;
        }
        catch
        {
            _isAuthenticated = false;
            _username = null;
            return false;
        }
    }

    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "username");
        _isAuthenticated = false;
        _username = null;
    }

    public async Task<string?> GetTokenAsync()
    {
        if (!_isAuthenticated)
            return null;

        return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
    }
}
