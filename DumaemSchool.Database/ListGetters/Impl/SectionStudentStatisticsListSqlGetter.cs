using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.DataManipulation;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.ListGetters.Impl;

public sealed class SectionStudentStatisticsListSqlGetter : AbstractListSqlGenerator<SectionStudentStatistics>
{
    public SectionStudentStatisticsListSqlGetter(IEntityMapping<SectionStudentStatistics> mapping) : base(mapping)
    {
    }

    public override ListQuery GetListSql(ListParam param)
    {
        var parameters = new DynamicParameters();
        var sectionIdFilterDefinition =
            param.Filters.First(x => x.FieldName == nameof(SectionStudentStatistics.SectionId));
        var sectionIdFilter = SqlUtility.GetFilterToSql(sectionIdFilterDefinition, parameters, PropertyToSqlMapping)!;

        return new ListQuery
        {
            SelectSql = $@"WITH student_statistics AS (SELECT ss.student_id,
                                                              count(l.id)                lessons_to_visit,
                                                              count(attendance.*)        attendance,
                                                              count(positive_activity.*) positive_activity,
                                                              count(negative_activity.*) negative_activity
                                                       FROM public.lesson l
                                                                JOIN public.schedule sch
                                                                     ON sch.id = l.schedule_id
                                                                JOIN public.section_student ss
                                                                     ON ss.section_id = sch.section_id
                                                                         AND ss.is_actual
                                                                LEFT JOIN public.activity positive_activity
                                                                          ON positive_activity.student_id = ss.student_id
                                                                              AND positive_activity.lesson_id = l.id
                                                                              AND positive_activity.mark = 1
                                                                LEFT JOIN public.activity negative_activity
                                                                          ON negative_activity.student_id = ss.student_id
                                                                              AND negative_activity.lesson_id = l.id
                                                                              AND negative_activity.mark = -1
                                                                LEFT JOIN public.attendance attendance
                                                                          ON attendance.student_id = ss.student_id
                                                                              AND attendance.lesson_id = l.id
                                                       WHERE {sectionIdFilter.SqlText}
                                                         AND ss.date_added <= l.date
                                                       GROUP BY ss.student_id)
                            SELECT ss.student_id StudentId,
                                   s.name StudentName,
                                   ss.lessons_to_visit PlanVisitedLessonsCount,
                                   ss.lessons_to_visit - ss.attendance FactVisitedLessonsCount,
                                   ss.positive_activity PositiveMarksCount,
                                   ss.negative_activity NegativeMarksCount
                            FROM student_statistics ss
                                     JOIN public.student s ON s.id = ss.student_id",
            CountSql = "",
            Parameters = parameters
        };
    }
}