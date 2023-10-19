using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.Mappers.EntityMapping;

public sealed class StudentLessonStatisticsEnityMapping : AbstractEntityMapping<StudentLessonStatistics>
{
    public StudentLessonStatisticsEnityMapping()
    {
        Map(nameof(StudentLessonStatistics.Id), "ss.id", isPrimaryKey: true);
        Map(nameof(StudentLessonStatistics.StudentId), "ss.student_id");
        Map(nameof(StudentLessonStatistics.StudentName), "ss.name");
        Map(nameof(StudentLessonStatistics.LessonId), "ss.lesson_id");

        Map(nameof(StudentLessonStatistics.ActivityMark), "COALESCE(activity.mark, 0)");
        Map(nameof(StudentLessonStatistics.WasAttended), "attendance.id is null");
    }
}