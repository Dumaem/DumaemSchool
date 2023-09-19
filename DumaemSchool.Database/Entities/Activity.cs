namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Активность ученика
/// </summary>
public class Activity
{
    public int Id { get; set; }

    public int LessonId { get; set; }

    public int StudentId { get; set; }

    /// <summary>
    /// Оценка активности
    /// </summary>
    public int Mark { get; set; }

    public virtual Lesson Lesson { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
