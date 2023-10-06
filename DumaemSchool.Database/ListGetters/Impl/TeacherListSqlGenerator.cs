using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers;
using DumaemSchool.Database.Mappers.EntityMapping;

namespace DumaemSchool.Database.ListGetters.Impl;

public class TeacherListSqlGenerator : IListSqlGenerator<TeacherDto>
{
    private string SelectString =>
        string.Join(", ", Mapping.GetMapping().Select(x => $"{x.Value} {x.Key}"));

    public TeacherListSqlGenerator(IEntityMapping<TeacherDto> mapping)
    {
        Mapping = mapping;
    }

    public IEntityMapping<TeacherDto> Mapping { get; }

    public ListQuery GetListSql(ListParam param)
    {
        var defaultMapping = Mapping.GetMapping();
        var dynamicParams = new DynamicParameters();

        var where = string.Empty;
        var having = string.Empty;
        if (param.Filters.Any())
        {
            var presentFilters = param.Filters
                .Where(x => Mapping.DefaultMappingPropertyNames.Contains(x.FieldName))
                .ToArray();
            if (presentFilters.Any())
            {
                where = $" WHERE {string.Join(" AND ", presentFilters
                    .Select(x => SqlUtility.GetFilterToSql(x, dynamicParams, defaultMapping)))} ";
            }

            presentFilters = param.Filters
                .Where(x => Mapping.AggregateMappingPropertyNames.Contains(x.FieldName))
                .ToArray();
            if (presentFilters.Any())
            {
                having = $" HAVING {string.Join(" AND ", presentFilters
                    .Select(x => SqlUtility.GetFilterToSql(x, dynamicParams, defaultMapping)))} ";
            }
        }

        var sort = param.Sorting.Any()
            ? $" ORDER BY {string.Join(", ", param.Sorting.Select(x => SqlUtility.GetSortingToSql(x, defaultMapping)))} "
            : " ORDER BY id ";

        var pagination =
            $" LIMIT {param.Pagination.ItemCount} OFFSET {param.Pagination.PageNumber * param.Pagination.ItemCount} ";


        return new ListQuery
        {
            SelectSql = @$"SELECT {SelectString}
                  FROM public.teacher t 
                      JOIN public.section_teacher st
                          ON t.id = st.teacher_id
                  {where}
                  GROUP BY 1, 2, 3
                  {having}
                  {sort}
                  {pagination}",
            CountSql = $@"SELECT {SelectString}
                  FROM public.teacher t 
                      JOIN public.section_teacher st
                          ON t.id = st.teacher_id
                  {where}
                  GROUP BY 1, 2, 3
                  {having}",
            Parameters = dynamicParams
        };
    }
}