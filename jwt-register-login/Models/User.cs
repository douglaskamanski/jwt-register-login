using jwt_register_login.Enum;

namespace jwt_register_login.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public Position Position { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public DateTime TokenCreationDate { get; set; } = DateTime.Now;
}
