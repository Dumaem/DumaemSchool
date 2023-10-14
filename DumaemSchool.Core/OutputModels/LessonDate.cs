namespace DumaemSchool.Core.OutputModels;

public sealed class LessonDate
{
    public int LessonId { get; set; }
    public DateOnly ConductionDate { get; set; }
}