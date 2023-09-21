namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Учитель
/// </summary>
public sealed class Teacher
{
    public int Id { get; set; }

    /// <summary>
    /// Дата увольнения
    /// </summary>
    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateOnly? DateDeleted { get; set; }

    public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public ICollection<SectionTeacher> SectionTeachers { get; set; } = new List<SectionTeacher>();

    public ICollection<TeacherLink> TeacherLinks { get; set; } = new List<TeacherLink>();
}
