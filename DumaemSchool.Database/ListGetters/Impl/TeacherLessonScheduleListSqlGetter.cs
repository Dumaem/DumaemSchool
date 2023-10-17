using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.ListGetters.Impl;

public sealed class TeacherLessonScheduleListSqlGetter : AbstractListSqlGenerator<LessonForScheduler>
{
    public TeacherLessonScheduleListSqlGetter(IEntityMapping<LessonForScheduler> mapping) : base(mapping)
    {
    }

    public override ListQuery GetListSql(ListParam param)
    {
        var dynamicParams = new DynamicParameters();
        var where = GetWhereExpression(param.Filters, dynamicParams);
        var sort = GetOrderByExpression(param.Sorting);

        return new ListQuery
        {
            SelectSql = $@"SELECT {SelectString}
                           FROM public.lesson l
                                    JOIN public.schedule schedule
                                         ON schedule.id = l.schedule_id
                                    JOIN public.section section
                                         ON section.id = schedule.section_id
                                    JOIN public.section_teacher ss
                                         ON section.id = ss.section_id
                                             AND ss.is_actual
                           WHERE TRUE {where}
                           ORDER BY {sort}",
            CountSql = "",
            Parameters = dynamicParams
        };
    }
}