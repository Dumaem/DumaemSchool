namespace DumaemSchool.Database.Entities;

/// <summary>
/// Связка между пользователем учителя и бизнес-сущностью
/// </summary>
public class TeacherLink
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TeacherId { get; set; }

    public virtual Teacher Teacher { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
