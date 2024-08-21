using System.Diagnostics.CodeAnalysis;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;

namespace LiveVoting.WasmClient.StateManagement;

public class AuthStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly NavigationManager _navigationManager;

    public bool IsAuthenticated { get; private set; }
    public event Action OnAuthStateChanged;
    
    public AuthStateProvider(ILocalStorageService localStorage, NavigationManager navigationManager)
    {
        _localStorage = localStorage;
        _navigationManager = navigationManager;
    }

    public async Task CheckAuthStateAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("jwt");
        IsAuthenticated = !string.IsNullOrEmpty(token);
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