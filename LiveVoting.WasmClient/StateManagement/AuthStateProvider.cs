using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using Blazored.LocalStorage;
using LiveVoting.WasmClient.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace LiveVoting.WasmClient.StateManagement;

public class AuthStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly NavigationManager _navigationManager;
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;
    public bool IsAuthenticated { get; private set; }
    public event Action OnAuthStateChanged;
    
    public AuthStateProvider(ILocalStorageService localStorage, NavigationManager navigationManager, HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _localStorage = localStorage;
        _navigationManager = navigationManager;
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task CheckAuthStateAsync()
    {
        Console.WriteLine("Am intrat in check");
        var token = await _localStorage.GetItemAsync<string>("jwt");
        if (token == null)
        {
            await _jsRuntime.InvokeVoidAsync("sessionExpiredAlert", "Session expired, log in!");
            IsAuthenticated = false;
            OnAuthStateChanged?.Invoke();
            return;
        }
        var jwtToken = new JwtSecurityToken(token);
        var isInvalidToken = jwtToken.ValidFrom > DateTime.UtcNow || jwtToken.ValidTo < DateTime.UtcNow;
        Console.WriteLine("Is invalid? " + isInvalidToken);
        if (isInvalidToken)
        {
            await _localStorage.RemoveItemAsync("jwt");
            //fa call la logout
            var guidClaim = JwtService.GetGuidClaimFromToken(token);
            var uri = $"/api/logout/{guidClaim}";
            var response = await _httpClient.PostAsJsonAsync(uri, "");
            
            await _jsRuntime.InvokeVoidAsync("sessionExpiredAlert", "Session expired, log in!", uri);
            
            IsAuthenticated = false;
            OnAuthStateChanged?.Invoke();
            return;
        }
        
        IsAuthenticated = true;
        OnAuthStateChanged?.Invoke();
    }

    public async Task LogoutAsync()
    {
        await _localStorage.RemoveItemAsync("jwt");
        IsAuthenticated = false;
        OnAuthStateChanged?.Invoke();
        _navigationManager.NavigateTo("/login");
        
    }
}