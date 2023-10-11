namespace DumaemSchool.Core.OutputModels;

public sealed class SectionStudent
{
    public int Id { get; set; }

    public int StudentId { get; set; }
    public string StudentName { get; set; } = null!;

    public int SectionId { get; set; }
    public string SectionGroupName { get; set; } = null!;

    public DateOnly DateAdded { get; set; }
}