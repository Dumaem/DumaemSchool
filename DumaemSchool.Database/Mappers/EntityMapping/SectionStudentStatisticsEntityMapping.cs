using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.Mappers.EntityMapping;

public sealed class SectionStudentStatisticsEntityMapping : AbstractEntityMapping<SectionStudentStatistics>
{
    public SectionStudentStatisticsEntityMapping()
    {
        Map(nameof(SectionStudentStatistics.StudentId), "ss.student_id");
        Map(nameof(SectionStudentStatistics.StudentName), "s.name");
        Map(nameof(SectionStudentStatistics.PlanVisitedLessonsCount), "ss.lessons_to_visit");
        Map(nameof(SectionStudentStatistics.FactVisitedLessonsCount), "ss.lessons_to_visit - ss.attendance");
        Map(nameof(SectionStudentStatistics.PositiveMarksCount), "ss.positive_activity");
        Map(nameof(SectionStudentStatistics.PositiveMarksCount), "ss.negative_activity");
    }
}