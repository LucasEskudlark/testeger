using Testeger.Shared.Models.Entities;

namespace Testeger.Shared.Helpers;

public static class BreadCrumbHelper
{
    public static string GetProjectUrlBasedOnRequest(TestRequest request) => $"/project/{request.ProjectId}";
    public static string GetProjectUrl(string id) => $"/project/{id}";
    public static string GetRequestUrl(TestRequest request) => $"/test-request/{request.Id}";
    public static string GetRequestText(TestRequest request) => $"Request n°{request.Number}";
}
