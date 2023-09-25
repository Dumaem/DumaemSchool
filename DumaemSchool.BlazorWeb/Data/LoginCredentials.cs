namespace DumaemSchool.BlazorWeb.Data;

public class LoginCredentials
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool RememberMe { get; set; }
    
    public bool IsLoginWithPassword { get; set; }
}