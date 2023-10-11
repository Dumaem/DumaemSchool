using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.ListGetters.Impl;

public sealed class SectionScheduleListSqlGetter : AbstractListSqlGenerator<SectionSchedule>
{
    public SectionScheduleListSqlGetter(IEntityMapping<SectionSchedule> mapping) : base(mapping)
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
                           FROM public.schedule schedule
                                JOIN public.section sec ON schedule.section_id = sec.id
                           WHERE TRUE {where}
                           ORDER BY {sort}",
            CountSql = $@"SELECT {SelectString} 
                           FROM public.schedule schedule
                                JOIN public.section sec ON schedule.section_id = sec.id
                           WHERE TRUE {where}
                           ORDER BY {sort}",
            Parameters = dynamicParams
        };
    }
}