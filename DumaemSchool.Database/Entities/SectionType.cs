namespace DumaemSchool.Database.Entities;

/// <summary>
/// Сущность Вид кружка
/// </summary>
public sealed class SectionType
{
    public int Id { get; set; }

    /// <summary>
    /// Название вида кружка
    /// </summary>
    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateOnly? DateDeleted { get; set; }

    public ICollection<Section> Sections { get; set; } = new List<Section>();
}
