namespace DumaemSchool.Database.Entities;

/// <summary>
/// Пользователь системы
/// </summary>
public class User
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public string Password { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<TeacherLink> TeacherLinks { get; set; } = new List<TeacherLink>();
}
