using jwt_register_login.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace jwt_register_login.Services.PasswordService;

public class PasswordService : IPasswordInterface
{
    private readonly IConfiguration _config;

    public PasswordService(IConfiguration config)
    {
        _config = config;
    }

    public void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public bool CheckPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512(passwordSalt))
        {
            var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            return computeHash.SequenceEqual(passwordHash);
        }
    }

    public string GenerateToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim("Position", user.Position.ToString()),
            new Claim("Email", user.Email),
            new Claim("Username", user.Username)
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
                                        claims: claims,
                                        expires: DateTime.Now.AddDays(1),
                                        signingCredentials: credentials);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}
