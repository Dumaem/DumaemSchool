namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Кружок
/// </summary>
public class Section
{
    public int Id { get; set; }

    /// <summary>
    /// Название группы студентов
    /// </summary>
    public string GroupName { get; set; } = null!;

    public int SectionTypeId { get; set; }

    public bool IsDeleted { get; set; }

    public DateOnly? DateDeleted { get; set; }

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual ICollection<SectionStudent> SectionStudents { get; set; } = new List<SectionStudent>();

    public virtual ICollection<SectionTeacher> SectionTeachers { get; set; } = new List<SectionTeacher>();

    public virtual SectionType SectionType { get; set; } = null!;
}
