namespace DumaemSchool.Database.Entities;

/// <summary>
/// Связка кружка и учителя, который его проводит
/// </summary>
public sealed class SectionTeacher
{
    public int Id { get; set; }

    public int SectionId { get; set; }

    public int TeacherId { get; set; }

    /// <summary>
    /// Метка актуальности записи
    /// </summary>
    public bool? IsActual { get; set; }

    public Section Section { get; set; } = null!;

    public Teacher Teacher { get; set; } = null!;
}
