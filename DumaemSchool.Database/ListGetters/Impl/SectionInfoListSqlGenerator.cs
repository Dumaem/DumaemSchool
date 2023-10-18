using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.ListGetters.Impl;

public sealed class SectionInfoListSqlGenerator : AbstractListSqlGenerator<SectionInfo>
{
    public SectionInfoListSqlGenerator(IEntityMapping<SectionInfo> mapping) : base(mapping)
    {
    }

    public override ListQuery GetListSql(ListParam param)
    {
        var dynamicParams = new DynamicParameters();

        var where = GetWhereExpression(param.Filters, dynamicParams);
        var having = GetHavingExpression(param.Filters, dynamicParams);
        var sort = GetOrderByExpression(param.Sorting);
        var havingString = string.IsNullOrWhiteSpace(having) ? "" : $"HAVING {having}";
        var pagination = GetPaginationExpression(param.Pagination);

        return new ListQuery
        {
            Parameters = dynamicParams,
            CountSql = $"""
                        WITH t AS (SELECT {SelectString}
                                   FROM public.section s
                                      JOIN public.section_type stype
                                          ON s.section_type_id = stype.id
                                      JOIN public.section_teacher steacher
                                          ON s.id = steacher.section_id
                                                 AND steacher.is_actual
                                      JOIN public.teacher t 
                                          ON steacher.teacher_id = t.id
                                      LEFT JOIN public.schedule schedule 
                                          ON s.id = schedule.section_id
                                   WHERE TRUE {where}
                                   GROUP BY 1, 2, 3, 4, 5
                                   {havingString}
                                   ORDER BY {sort})
                        SELECT COUNT(*) FROM t
                        """,
            SelectSql = $"""
                         SELECT {SelectString}
                         FROM public.section s
                             JOIN public.section_type stype
                                 ON s.section_type_id = stype.id
                             JOIN public.section_teacher steacher
                                 ON s.id = steacher.section_id
                                        AND steacher.is_actual
                             JOIN public.teacher t 
                                 ON steacher.teacher_id = t.id
                             LEFT JOIN public.schedule schedule 
                                 ON s.id = schedule.section_id
                         WHERE TRUE {where}
                         GROUP BY 1, 2, 3, 4, 5
                         {havingString}
                         ORDER BY {sort}
                         {pagination}
                         """
        };
    }
}