namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Занятие
/// </summary>
public sealed class Lesson
{
    public int Id { get; set; }

    public int ScheduleId { get; set; }

    public DateOnly Date { get; set; }

    /// <summary>
    /// Учитель, фактически проводивший занятие (может расходиться со связкой к кружку)
    /// </summary>
    public int TeacherId { get; set; }

    /// <summary>
    /// Было или будет ли проведено занятие
    /// </summary>
    public bool? IsConducted { get; set; }

    public ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public Schedule Schedule { get; set; } = null!;

    public Teacher Teacher { get; set; } = null!;
}
