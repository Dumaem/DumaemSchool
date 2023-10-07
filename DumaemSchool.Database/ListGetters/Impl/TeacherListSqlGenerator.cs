using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.ListGetters.Impl;

public sealed class TeacherListSqlGenerator : AbstractListSqlGenerator<TeacherDto>
{
    public TeacherListSqlGenerator(IEntityMapping<TeacherDto> mapping) : base(mapping)
    {
    }

    public override ListQuery GetListSql(ListParam param)
    {
        var dynamicParams = new DynamicParameters();

        var where = GetWhereExpression(param.Filters, dynamicParams);
        var having = GetHavingExpression(param.Filters, dynamicParams);
        var sort = GetOrderByExpression(param.Sorting);
        var pagination = GetPaginationExpression(param.Pagination);

        var havingString = string.IsNullOrEmpty(having) ? "" : $"HAVING {having}";

        return new ListQuery
        {
            SelectSql = @$"SELECT {SelectString}
                  FROM public.teacher t 
                      LEFT JOIN public.section_teacher st
                          ON t.id = st.teacher_id
                            AND st.is_actual
                  WHERE TRUE {where}
                  GROUP BY 1, 2, 3
                  {havingString}
                  ORDER BY {sort}
                  {pagination}",
            CountSql = $@"WITH t AS (SELECT {SelectString}
                  FROM public.teacher t 
                      LEFT JOIN public.section_teacher st
                          ON t.id = st.teacher_id
                            AND st.is_actual
                  WHERE TRUE {where}
                  GROUP BY 1, 2, 3
                  {havingString})
                  SELECT COUNT(*) FROM t",
            Parameters = dynamicParams
        };
    }
}