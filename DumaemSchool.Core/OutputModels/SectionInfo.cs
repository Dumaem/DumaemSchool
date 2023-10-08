namespace DumaemSchool.Core.OutputModels;

public sealed class SectionInfo
{
    public int Id { get; set; }
    public string GroupName { get; set; } = null!;
    public string TypeName { get; set; } = null!;
    public int LessonPerWeek { get; set; }

    public int TeacherId { get; set; }
    public string TeacherName { get; set; } = null!;
}