using System.Security.Claims;
using TicTacToe.BLL.Exceptions;

namespace TicTacToe.BLL.Extensions;

public static class IdentityExtension
{
    public static Guid GetUserId(this ClaimsPrincipal claims)
    {
        return Guid.Parse(claims.FindClaim(ClaimTypes.NameIdentifier));
    }

    private static string FindClaim(this ClaimsPrincipal claims, string claimName)
    {
        var claimsIdentity = claims.Identity as ClaimsIdentity;

        var claim = claimsIdentity?.FindFirst(claimName);

        if (claim == null)
        {
            throw new AppException("Can not get user id from the token.");
        }
        return claim.Value;

    }
}