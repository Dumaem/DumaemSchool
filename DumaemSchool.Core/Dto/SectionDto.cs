namespace DumaemSchool.Core.Dto;

public sealed class SectionDto
{
    public int Id { get; set; }

    /// <summary>
    /// Название группы студентов
    /// </summary>
    public string GroupName { get; set; } = null!;

    public string SectionTypeName { get; set; }

    public string TeacherName { get; set; } = null!;
}