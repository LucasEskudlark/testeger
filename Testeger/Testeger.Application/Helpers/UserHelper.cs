using System.Security.Claims;

namespace Testeger.Application.Helpers;

public static class UserHelper
{
    public static string GetUserId(ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value 
            ?? throw new InvalidOperationException("UserId can not be null!");

        return userId;
    }
}
