﻿using System.ComponentModel.DataAnnotations;

namespace DumaemSchool.BlazorWeb.Data;

public class TeacherCreateCredentials
{
    [Required(ErrorMessage = "Email должен быть заполнен")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имя должно быть заполнено")]
    public string Name { get; set; }
}