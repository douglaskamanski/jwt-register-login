using jwt_register_login.Data.Dtos;
using jwt_register_login.Services.AuthServices;
using Microsoft.AspNetCore.Mvc;

namespace jwt_register_login.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthInterface _authInterface;

    public AuthController(IAuthInterface authInterface)
    {
        _authInterface = authInterface;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(CreateUserDto userCreate)
    {
        var response = await _authInterface.Register(userCreate);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserDto loginUser)
    {
        var response = await _authInterface.Login(loginUser);

        return Ok(response);
    }

}
