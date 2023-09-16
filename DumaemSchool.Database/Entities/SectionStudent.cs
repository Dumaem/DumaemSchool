namespace DumaemSchool.Database.Entities;

/// <summary>
/// Связка кружка и ученика, который в нем занимается
/// </summary>
public class SectionStudent
{
    public int Id { get; set; }

    public int SectionId { get; set; }

    public int StudentId { get; set; }

    public virtual Section Section { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
