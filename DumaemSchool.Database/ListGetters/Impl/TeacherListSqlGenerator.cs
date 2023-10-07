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

        return new ListQuery
        {
            SelectSql = @$"SELECT {SelectString}
                  FROM public.teacher t 
                      JOIN public.section_teacher st
                          ON t.id = st.teacher_id
                            AND st.is_actual
                  WHERE {where}
                  GROUP BY 1, 2, 3
                  HAVING {having}
                  ORDER BY {sort}
                  {pagination}",
            CountSql = $@"SELECT {SelectString}
                  FROM public.teacher t 
                      JOIN public.section_teacher st
                          ON t.id = st.teacher_id
                            AND st.is_actual
                  WHERE {where}
                  GROUP BY 1, 2, 3
                  HAVING {having}",
            Parameters = dynamicParams
        };
    }
}