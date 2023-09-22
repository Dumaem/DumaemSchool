namespace DumaemSchool.Core.Models;

public sealed class Teacher
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    /// <summary>
    /// Дата увольнения
    /// </summary>
    public DateOnly? DateDeleted { get; set; }
}