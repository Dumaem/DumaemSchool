namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Ученик
/// </summary>
public class Student
{
    public int Id { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    public string Name { get; set; } = null!;

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<SectionStudent> SectionStudents { get; set; } = new List<SectionStudent>();
}
