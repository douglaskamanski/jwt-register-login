using jwt_register_login.Models;

namespace jwt_register_login.Services.PasswordService;

public interface IPasswordInterface
{
    void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool CheckPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    string GenerateToken(User user);
}
