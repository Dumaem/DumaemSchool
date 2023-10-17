namespace DumaemSchool.BlazorWeb.Data;

public class SectionScheduleCreateCredentials
{
    [Required(ErrorMessage = "День недели должен быть заполнен")]
    public DayOfWeek DayOfWeek { get; set; }
    
    [Required(ErrorMessage = "Время проведения занятия должно быть заполнено")]
    public TimeSpan? Time { get; set; }
    
    [Required(ErrorMessage = "Продолжительность занятия должна быть заполнено")]
    public int? Duration { get; set; }
    
    [Required(ErrorMessage = "Кабинет должен быть заполнен")]
    public int? Cabinet { get; set; }
}