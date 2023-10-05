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

    private static readonly string SelectString =
        string.Join(", ", PropertyToDatabaseMap.Select(x => $"{x.Value} \"{x.Key}\""));

    public ListQuery GetListSql(ListParam param)
    {
        var dynamicParams = new DynamicParameters();

        var where = string.Empty;
        if (param.Filters.Any())
        {
            where =
                $" WHERE {string.Join(" AND ", param.Filters.Select(x => SqlUtility.GetFilterToSql(x, dynamicParams, PropertyToDatabaseMap)))} ";
        }

        var sort = param.Sorting.Any()
            ? $" ORDER BY {string.Join(", ", param.Sorting.Select(x => SqlUtility.GetSortingToSql(x, PropertyToDatabaseMap)))} "
            : " ORDER BY id ";

        var pagination =
            $" LIMIT {param.Pagination.ItemCount} OFFSET {param.Pagination.PageNumber * param.Pagination.ItemCount} ";

        return new ListQuery
        {
            Sql = @$"SELECT {SelectString}
                  FROM public.teacher t 
                      JOIN public.section_teacher st
                          ON t.id = st.teacher_id
                  {where}
                  GROUP BY 1, 2, 3
                  {sort}
                  {pagination}",
            Parameters = dynamicParams
        };
    }
}