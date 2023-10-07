using System.Data;
using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Database.Mappers;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.ListGetters;

public abstract class AbstractListSqlGenerator<T> : IListSqlGenerator<T> where T : class
{
    protected string SelectString =>
        string.Join(", ", PropertyToSqlMapping.Select(x => $"{x.Value} {x.Key}"));

    protected IEntityMapping<T> Mapping { get; }

    protected IReadOnlyDictionary<string, string> PropertyToSqlMapping { get; }

    protected AbstractListSqlGenerator(IEntityMapping<T> mapping)
    {
        if (mapping.PropertyMappings.Count(x => x.IsPrimaryKey) != 1)
        {
            throw new DataException($"Entity of type {typeof(T).Name} has wrong primary key definition");
        }

        Mapping = mapping;
        PropertyToSqlMapping = mapping.GetMapping();
    }

    public abstract ListQuery GetListSql(ListParam param);

    protected string GetWhereExpression(FilterDefinition[] filters, DynamicParameters dynamicParams)
    {
        if (filters.Length == 0)
            return "";

        var presentFilters = filters
            .Where(x => Mapping.DefaultMappingPropertyNames.Contains(x.FieldName))
            .ToArray();
        if (presentFilters.Length == 0)
            return "";

        return $" AND {string.Join(" AND ", presentFilters
            .Select(x => SqlUtility.GetFilterToSql(x, dynamicParams, PropertyToSqlMapping)))} ";
    }

    protected string GetHavingExpression(FilterDefinition[] filters, DynamicParameters dynamicParams)
    {
        if (filters.Length == 0)
            return "";

        var presentFilters = filters
            .Where(x => Mapping.AggregateMappingPropertyNames.Contains(x.FieldName))
            .ToArray();
        if (presentFilters.Length == 0)
            return "";

        return $" {string.Join(" AND ", presentFilters
            .Select(x => SqlUtility.GetFilterToSql(x, dynamicParams, PropertyToSqlMapping)))} ";
    }

    protected string GetOrderByExpression(SortingDefinition[] sortingDefinitions)
    {
        return sortingDefinitions.Any()
            ? $" {string.Join(", ", sortingDefinitions.Select(x => SqlUtility.GetSortingToSql(x, PropertyToSqlMapping)))} "
            : $" {Mapping.PropertyMappings.First(x => x.IsPrimaryKey).SqlExpression} ";
    }

    protected string GetPaginationExpression(PaginationInfo paginationInfo)
    {
        return $" LIMIT {paginationInfo.ItemCount} OFFSET {paginationInfo.PageNumber * paginationInfo.ItemCount} ";
    }
}