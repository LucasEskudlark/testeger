using System.Net.Http.Json;
using Testeger.Shared.DTOs.Responses.TestCase;

namespace Testeger.Client.Services.TestCases;

public class TestCaseServiceNV : BaseService, ITestCaseServiceNV
{
    private const string BaseAddress = "api/testcases";
    public TestCaseServiceNV(HttpClient httpClient) : base(httpClient)
    {
    }

    public event Action? OnChange;
    public event Action? OnTestCaseAdded;
    public event Action? OnTestCaseDeleted;
    public event Action? OnTestCaseUpdated;

    public async Task<IEnumerable<GetTestCaseResponse>> GetTestCasesByTestRequestIdPagedAsync(string testRequestId)
    {
        var address = BaseAddress + $"/request/{testRequestId}";
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<GetTestCaseResponse>>(address);

        return response;
    }

    public async Task<GetTestCaseResponse> GetTestCaseByIdAsync(string id)
    {
        var address = BaseAddress + $"/{id}";
        var response = await _httpClient.GetFromJsonAsync<GetTestCaseResponse>(address);

        return response;
    }
}
