namespace DumaemSchool.Core.OutputModels;

public sealed class SectionStudentStatistics
{
    public int Id { get; set; }
    
    public int SectionId { get; set; }
    public string SectionName { get; set; } = null!;
    
    public int StudentId { get; set; }
    public string StudentName { get; set; } = null!;

    public int PlanVisitedLessonsCount { get; set; }
    public int FactVisitedLessonsCount { get; set; }
    
    public int PositiveMarksCount { get; set; }
    public int NegativeMarksCount { get; set; }
}