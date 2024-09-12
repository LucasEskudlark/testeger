using System.Security.Claims;

namespace Testeger.Application.Extensions;

public static class UserExtensions
{
    public static string GetUserId(this ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value 
            ?? throw new InvalidOperationException("UserId can not be null!");

        return userId;
    }
}
