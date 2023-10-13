using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.DataManipulation;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.ListGetters.Impl;

public sealed class StudentLessonStatisticsListSqlGetter : AbstractListSqlGenerator<StudentLessonStatistics>
{
    public StudentLessonStatisticsListSqlGetter(IEntityMapping<StudentLessonStatistics> mapping) : base(mapping)
    {
    }

    public override ListQuery GetListSql(ListParam param)
    {
        var dynamicParams = new DynamicParameters();
        var lessonIdFilterDefinition =
            param.Filters.First(x => x.FieldName == nameof(StudentLessonStatistics.LessonId));
        var lessonIdFilter = SqlUtility.GetFilterToSql(lessonIdFilterDefinition, dynamicParams,
            new Dictionary<string, string> { {nameof(StudentLessonStatistics.LessonId), "l.id"} })!;

        var filters = param.Filters
            .Where(x => x != lessonIdFilterDefinition)
            .ToArray();
        var where = GetWhereExpression(filters, dynamicParams);
        var sort = GetOrderByExpression(param.Sorting);

        return new ListQuery
        {
            SelectSql = $@"WITH section_students AS (
                                SELECT DISTINCT ss.id, ss.student_id, s.name, l.id lesson_id
                                FROM public.lesson l 
                                    JOIN public.schedule sch 
                                        ON sch.id = l.schedule_id 
                                    JOIN public.section_student ss
                                        ON ss.section_id = sch.section_id AND ss.is_actual
                                    JOIN public.student s
                                        ON s.id = ss.student_id
                                WHERE {lessonIdFilter.SqlText} AND ss.date_added <= l.date
                           )
                           SELECT {SelectString} 
                           FROM section_students ss
                                LEFT JOIN public.activity activity 
                                    ON activity.student_id = ss.student_id 
                                        AND activity.lesson_id = ss.lesson_id
                                LEFT JOIN public.attendance attendance 
                                    ON attendance.lesson_id = ss.lesson_id
                                        AND attendance.lesson_id = ss.lesson_id
                           WHERE TRUE {where}
                           ORDER BY {sort}",
            CountSql = $@"",
            Parameters = dynamicParams
        };
    }
}