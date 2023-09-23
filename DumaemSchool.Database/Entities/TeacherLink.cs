namespace DumaemSchool.Database.Entities;

/// <summary>
/// Связка между пользователем учителя и бизнес-сущностью
/// </summary>
public sealed class TeacherLink
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TeacherId { get; set; }

    public Teacher Teacher { get; set; } = null!;

    public User User { get; set; } = null!;
}
