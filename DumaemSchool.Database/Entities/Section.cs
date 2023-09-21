namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Кружок
/// </summary>
public sealed class Section
{
    public int Id { get; set; }

    /// <summary>
    /// Название группы студентов
    /// </summary>
    public string GroupName { get; set; } = null!;

    public int SectionTypeId { get; set; }

    public bool IsDeleted { get; set; }

    public DateOnly? DateDeleted { get; set; }

    public ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public ICollection<SectionStudent> SectionStudents { get; set; } = new List<SectionStudent>();

    public ICollection<SectionTeacher> SectionTeachers { get; set; } = new List<SectionTeacher>();

    public SectionType SectionType { get; set; } = null!;
}
