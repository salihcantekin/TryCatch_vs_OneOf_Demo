using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneOf.WebApi.Models;
using OneOf.WebApi.Services;

namespace OneOf.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    readonly UserService userService = new();

    [HttpGet("login")]
    public IActionResult Login([FromQuery]UserLoginRequest loginRequest)
    {
        var response = userService.Login_WithOneOf(loginRequest);
        
        return response.Match(
                                response => Ok(response),
                                error => StatusCode(StatusCodes.Status204NoContent, error.Message)
                             );
    }

    [HttpGet("login_exception")]
    public IActionResult Login2([FromQuery] UserLoginRequest loginRequest)
    {
        var response = userService.Login_WithException(loginRequest);

        return Ok(response);
    }
}
