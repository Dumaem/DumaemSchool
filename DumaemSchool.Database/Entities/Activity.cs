namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Активность ученика
/// </summary>
public sealed class Activity
{
    public int Id { get; set; }

    public int LessonId { get; set; }

    public int StudentId { get; set; }

    /// <summary>
    /// Оценка активности
    /// </summary>
    public int Mark { get; set; }

    public Lesson Lesson { get; set; } = null!;

    public Student Student { get; set; } = null!;
}
