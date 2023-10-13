namespace DumaemSchool.Core.Models;

public class Lesson
{
    public int Id { get; set; }

    public int ScheduleId { get; set; }

    public DateOnly Date { get; set; }
    
    public int TeacherId { get; set; }
    
    public bool? IsConducted { get; set; }
}