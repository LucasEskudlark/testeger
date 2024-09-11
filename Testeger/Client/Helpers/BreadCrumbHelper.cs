namespace Testeger.Client.Helpers;

public static class BreadCrumbHelper
{
    public static string GetProjectUrl(string id) => $"/project/{id}";
    public static string GetRequestUrl(string projectId, string requestId) => $"/project/{projectId}/test-request/{requestId}";
    public static string GetRequestText(int number) => $"Request n°{number}";
}
