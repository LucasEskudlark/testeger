using Testeger.Shared.Models.Entities;

namespace Testeger.Shared.Helpers;

public static class BreadCrumbHelper
{
    public static string GetProjectUrl(TestRequest request) => $"/project/{request.ProjectId}";
    public static string GetRequestUrl(TestRequest request) => $"/test-request/{request.Id}";
    public static string GetRequestText(TestRequest request) => $"Request N°{request.Number}";
}
