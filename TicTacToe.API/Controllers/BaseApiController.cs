using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.BLL.Extensions;

namespace TicTacToe.API.Controllers;

public class BaseApiController : ControllerBase
{
    protected Guid UserId => HttpContext.User.GetUserId();

}