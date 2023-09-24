using System.ComponentModel.DataAnnotations;

namespace DumaemSchool.BlazorWeb.Data;

public class LoginCredentials
{
    [Required(ErrorMessage = "Email должен быть заполнен")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Пароль должен быть заполнен")]
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
}