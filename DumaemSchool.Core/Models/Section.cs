namespace DumaemSchool.Core.Models;

/// <summary>
/// Сущность Кружок
/// </summary>
public sealed class Section
{
    public int Id { get; set; }

    /// <summary>
    /// Название группы студентов
    /// </summary>
    public string GroupName { get; set; } = null!;

    public int SectionTypeId { get; set; }

    public bool IsDeleted { get; set; }

    public DateOnly? DateDeleted { get; set; }
}
