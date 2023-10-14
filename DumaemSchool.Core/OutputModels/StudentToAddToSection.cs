namespace DumaemSchool.Core.OutputModels;

public sealed class StudentToAddToSection
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int SectionId { get; set; }
}