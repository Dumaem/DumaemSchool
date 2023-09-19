namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Учитель
/// </summary>
public class Teacher
{
    public int Id { get; set; }

    /// <summary>
    /// Дата увольнения
    /// </summary>
    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateOnly? DateDeleted { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual ICollection<SectionTeacher> SectionTeachers { get; set; } = new List<SectionTeacher>();

    public virtual ICollection<TeacherLink> TeacherLinks { get; set; } = new List<TeacherLink>();
}
