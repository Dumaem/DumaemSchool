namespace DumaemSchool.BlazorWeb.Data;

public class LoginCredentials
{
    [Required(ErrorMessage = "Email должен быть заполнен")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Пароль должен быть заполнен")]
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
    
    public bool IsLoginWithPassword { get; set; }
}