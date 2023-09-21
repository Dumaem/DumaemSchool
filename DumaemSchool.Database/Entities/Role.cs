namespace DumaemSchool.Database.Entities;

/// <summary>
/// Роль пользователя
/// </summary>
public sealed class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<User> Users { get; set; } = new List<User>();
}
