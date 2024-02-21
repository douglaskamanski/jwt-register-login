using jwt_register_login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jwt_register_login.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    [Authorize]
    [HttpGet]
    public ActionResult<Response<string>> GetUser()
    {
        Response<string> response = new Response<string>();
        response.Message = "You are in";

        return Ok(response);
    }
}
