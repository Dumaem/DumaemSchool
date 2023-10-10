namespace DumaemSchool.Database.Entities;

/// <summary>
/// Связка кружка и ученика, который в нем занимается
/// </summary>
public sealed class SectionStudent
{
    public int Id { get; set; }

    public int SectionId { get; set; }

    public int StudentId { get; set; }

    public DateOnly DateAdded { get; set; }

    public Section Section { get; set; } = null!;

    public Student Student { get; set; } = null!;
}
