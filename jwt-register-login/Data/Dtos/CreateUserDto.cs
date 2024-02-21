using jwt_register_login.Enum;
using System.ComponentModel.DataAnnotations;

namespace jwt_register_login.Data.Dtos;

public class CreateUserDto
{
    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
    [Compare("Password", ErrorMessage = "Password doesn't match")]
    public string PasswordConfirm { get; set; }
    [Required(ErrorMessage = "Position is required")]
    public Position Position { get; set; }
}
