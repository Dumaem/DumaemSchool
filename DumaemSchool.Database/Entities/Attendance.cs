namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Посещаемость ученика, наличие записи означает отсутствие ученика на занятии
/// </summary>
public class Attendance
{
    public int Id { get; set; }

    public int LessonId { get; set; }

    public int StudentId { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
