using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers;

namespace DumaemSchool.Database.ListGetters.Impl;

public class TeacherListSqlGenerator : IListSqlGenerator<TeacherDto>
{
    private static readonly Dictionary<string, string> PropertyToDatabaseMap = new()
    {
        { nameof(TeacherDto.Id), "t.id" },
        { nameof(TeacherDto.Name), "t.name" },
        { nameof(TeacherDto.IsDeleted), "is_deleted" },
        { nameof(TeacherDto.SectionsCount), "count(st.*)" },
    };

    private static readonly HashSet<string> AggregateColumns = new()
    {
        nameof(TeacherDto.SectionsCount)
    };

    private static readonly string SelectString =
        string.Join(", ", PropertyToDatabaseMap.Select(x => $"{x.Value} {x.Key}"));

    public ListQuery GetListSql(ListParam param)
    {
        var dynamicParams = new DynamicParameters();

        var where = string.Empty;
        if (param.Filters.Any())
        {
            where =
                $" WHERE {string.Join(" AND ", param.Filters
                    .Where(x => !AggregateColumns.Contains(x.FieldName))
                    .Select(x => SqlUtility.GetFilterToSql(x, dynamicParams, PropertyToDatabaseMap)))} ";
        }

        var sort = param.Sorting.Any()
            ? $" ORDER BY {string.Join(", ", param.Sorting.Select(x => SqlUtility.GetSortingToSql(x, PropertyToDatabaseMap)))} "
            : " ORDER BY id ";

        var pagination =
            $" LIMIT {param.Pagination.ItemCount} OFFSET {param.Pagination.PageNumber * param.Pagination.ItemCount} ";

        var having = string.Empty;
        var aggregateFilters = param.Filters
            .Where(x => AggregateColumns.Contains(x.FieldName))
            .ToList();
        if (aggregateFilters.Any())
        {
            having = $" HAVING {string.Join(" AND ", aggregateFilters
                .Select(x => SqlUtility.GetFilterToSql(x, dynamicParams, PropertyToDatabaseMap)))} ";
        }

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
            CountSql = "SELECT COUNT(*) FROM public.teacher t",
            Parameters = dynamicParams
        };
    }
}