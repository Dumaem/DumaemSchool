namespace DumaemSchool.Database.Entities;

/// <summary>
/// Пользователь системы
/// </summary>
public sealed class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Password { get; set; } = null!;

    public Role Role { get; set; } = null!;

    public ICollection<TeacherLink> TeacherLinks { get; set; } = new List<TeacherLink>();
}
