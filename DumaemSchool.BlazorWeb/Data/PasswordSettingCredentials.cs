namespace DumaemSchool.BlazorWeb.Data;

public class PasswordSettingCredentials
{
    [Required]
    [MinLength(5)]
    public string NewPassword { get; set; } = null!;
    
    [Required]
    [MinLength(5)]
    public string NewPasswordRepeat { get; set; } = null!;
}