using System.ComponentModel.DataAnnotations;

namespace jwt_register_login.Data.Dtos;

public class LoginUserDto
{
    [Required(ErrorMessage = "Email is required"), EmailAddress(ErrorMessage = "Invalid Email")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}
