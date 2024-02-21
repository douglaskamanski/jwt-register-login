using jwt_register_login.Data.Dtos;
using jwt_register_login.Models;

namespace jwt_register_login.Services.AuthServices;

public interface IAuthInterface
{
    Task<Response<CreateUserDto>> Register(CreateUserDto createUser);
    Task<Response<string>> Login(LoginUserDto loginUser);
}
