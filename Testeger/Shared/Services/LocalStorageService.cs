using Blazored.LocalStorage;
using Newtonsoft.Json;

namespace Testeger.Shared.Services;

public class LocalStorageService
{
    private readonly ILocalStorageService _localStorage;

    public LocalStorageService(ILocalStorageService localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<T?> ReadFromStorage<T>(string key)
    {
        var json = await _localStorage.GetItemAsStringAsync(key);
        if (string.IsNullOrEmpty(json))
        {
            return default;
        }

        var innerJson = JsonConvert.DeserializeObject<string>(json);
        return JsonConvert.DeserializeObject<T>(innerJson);
    }

    public async Task WriteToStorage<T>(string key, T data)
    {
        var jsonString = JsonConvert.SerializeObject(data);
        await _localStorage.SetItemAsync(key, jsonString);
    }

    public async Task ClearStorage()
    {
        await _localStorage.ClearAsync();
    }
}
