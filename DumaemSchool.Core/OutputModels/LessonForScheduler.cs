namespace DumaemSchool.Core.OutputModels;

public class LessonForScheduler
{
    public int LessonId { get; set; }

    public int TeacherId { get; set; }

    public DateTime LessonStart { get; set; }
    public TimeSpan Duration { get; set; }
    public string? SectionGroupName { get; set; }
    public DateTime LessonEnd => LessonStart.Add(Duration);
    public bool IsReplacement { get; set; }
    public bool IsCancelled { get; set; }
}