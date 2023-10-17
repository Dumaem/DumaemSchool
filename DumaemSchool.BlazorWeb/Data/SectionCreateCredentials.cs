namespace DumaemSchool.BlazorWeb.Data;

public class SectionCreateCredentials
{
    [Required(ErrorMessage = "Требуется указать учителя")]
    public TeacherDto? Teacher { get; set; }

    [Required(ErrorMessage = "Требуется указать вид секции")]
    public SectionType? SectionType { get; set; }

    [Required(ErrorMessage = "Требуется указать название группы")]
    public string GroupName { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Требуется добавить расписание")]
    public List<SectionSchedule> Schedules { get; set; } = new();
}