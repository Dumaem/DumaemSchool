namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Расписание
/// </summary>
public class Schedule
{
    public int Id { get; set; }

    public int SectionId { get; set; }

    /// <summary>
    /// День недели (0 - понедельник, 6 - воскресенье)
    /// </summary>
    public int DayOfWeek { get; set; }

    /// <summary>
    /// Время проведения
    /// </summary>
    public TimeOnly Time { get; set; }

    /// <summary>
    /// Кабинет, в котором будет проводиться занятие
    /// </summary>
    public int Cabinet { get; set; }

    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();

    public virtual Section Section { get; set; } = null!;
}
