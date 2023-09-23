namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Ученик
/// </summary>
public sealed class Student
{
    public int Id { get; set; }

    /// <summary>
    /// ФИО
    /// </summary>
    public string Name { get; set; } = null!;

    public ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public ICollection<SectionStudent> SectionStudents { get; set; } = new List<SectionStudent>();
}
