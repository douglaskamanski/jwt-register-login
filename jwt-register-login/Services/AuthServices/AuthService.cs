using jwt_register_login.Data;
using jwt_register_login.Data.Dtos;
using jwt_register_login.Models;
using jwt_register_login.Services.PasswordService;
using Microsoft.EntityFrameworkCore;

namespace jwt_register_login.Services.AuthServices;

public class AuthService : IAuthInterface
{
    private readonly AppDbContext _context;
    private readonly IPasswordInterface _passwordInterface;

    public AuthService(AppDbContext context, IPasswordInterface passwordInterface)
    {
        _context = context;
        _passwordInterface = passwordInterface;
    }

    public async Task<Response<CreateUserDto>> Register(CreateUserDto createUser)
    {
        Response<CreateUserDto> responseService = new Response<CreateUserDto>();

        try
        {
            if (!CheckEmailUserExists(createUser))
            {
                responseService.Data = null;
                responseService.Message = "Email/User already registered";
                responseService.Status = false;
                return responseService;
            }

            _passwordInterface.GeneratePasswordHash(createUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User()
            {
                Email = createUser.Email,
                Username = createUser.Username,
                Position = createUser.Position,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            responseService.Message = "User created with successfully!";

        } catch (Exception ex)
        {
            responseService.Data = null;
            responseService.Message = ex.Message;
            responseService.Status = false;
        }

        return responseService;
    }

    private bool CheckEmailUserExists(CreateUserDto createUser)
    {
        var user = _context.Users.FirstOrDefault(userDb => 
                                                    userDb.Email == createUser.Email ||
                                                    userDb.Username == createUser.Username);

        if (user != null) return false;

        return true;
    }

    public async Task<Response<string>> Login(LoginUserDto loginUser)
    {
        Response<string> responseService = new Response<string>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(userDb => userDb.Email == loginUser.Email);

            if (user == null)
            {
                responseService.Message = "Invalid credentials";
                responseService.Status = false;
                return responseService;
            }

            if (!_passwordInterface.CheckPasswordHash(loginUser.Password, user.PasswordHash, user.PasswordSalt))
            {
                responseService.Message = "Invalid credentials";
                responseService.Status = false;
                return responseService;
            }

            var token = _passwordInterface.GenerateToken(user);

            responseService.Data = token;
            responseService.Message = "User logged successfully";
        }
        catch (Exception ex)
        {
            responseService.Data = null;
            responseService.Message = ex.Message;
            responseService.Status = false;
        }

        return responseService;
    }
}
