namespace DumaemSchool.Core.OutputModels;

public sealed class SectionSchedule
{
    public int Id { get; set; }
    
    public int SectionId { get; set; }
    public string SectionGroupName { get; set; } = null!;
    
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly Time { get; set; }
    public TimeSpan Duration { get; set; }
    public int Cabinet { get; set; }
}