namespace DumaemSchool.Database.Entities;

/// <summary>
/// Связка кружка и учителя, который его проводит
/// </summary>
public class SectionTeacher
{
    public int Id { get; set; }

    public int SectionId { get; set; }

    public int TeacherId { get; set; }

    /// <summary>
    /// Метка актуальности записи
    /// </summary>
    public bool? IsActual { get; set; }

    public virtual Section Section { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
