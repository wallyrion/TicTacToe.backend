using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TicTacToe.API.Controllers;

public class BaseApiController : ControllerBase
{
    protected Guid UserId => Guid.Parse(FindClaim(ClaimTypes.NameIdentifier) );
    private string FindClaim(string claimName)
    {
        var claimsIdentity = HttpContext.User.Identity as ClaimsIdentity;

        var claim = claimsIdentity?.FindFirst(claimName);

        if (claim == null)
        {
            throw new Exception("Can not get user id from the token.");
        }
        return claim.Value;

    }

}