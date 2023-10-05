namespace DumaemSchool.Core.OutputModels;

public sealed class TeacherDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int SectionsCount { get; set; }
    public bool IsDeleted { get; set; }
}