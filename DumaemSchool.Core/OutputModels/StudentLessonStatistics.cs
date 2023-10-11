using DumaemSchool.Core.Models;

namespace DumaemSchool.Core.OutputModels;

public sealed class StudentLessonStatistics
{
    public int Id { get; set; }
    
    public int LessonId { get; set; }

    public int StudentId { get; set; }
    public string StudentName { get; set; } = null!;

    public bool WasAttended { get; set; }
    public LessonActivityMark ActivityMark { get; set; }
}