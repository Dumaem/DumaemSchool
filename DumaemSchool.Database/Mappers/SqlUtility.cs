using Dapper;
using DumaemSchool.Core.DataManipulation;

namespace DumaemSchool.Database.Mappers;

public static class SqlUtility
{
    private static readonly Dictionary<FilterOperand, string> OperandToSql = new()
    {
        { FilterOperand.Contains, "ILIKE" },
        { FilterOperand.NotContains, "NOT ILIKE" },
        { FilterOperand.Equal, "=" },
        { FilterOperand.NotEqual, "<>" },
        { FilterOperand.GreaterThan, ">" },
        { FilterOperand.LessThan, "<" },
        { FilterOperand.GreaterThanOrEqual, ">=" },
        { FilterOperand.LessThanOrEqual, "<=" },
    };

    public static string GetFilterToSql(FilterDefinition filter, DynamicParameters parameters, Dictionary<string, string>? propertyMap = null)
    {
        var fieldName = propertyMap?.TryGetValue(filter.FieldName, out var result) ?? false ? result : filter.FieldName;
        var value = filter.Value is string
            ? filter.Operand is FilterOperand.Contains ? $"%{filter.Value}%" : $"'{filter.Value}'"
            : filter.Value;
        var parameterKey = filter.ToString();
        parameters.Add(parameterKey, value);

        return $"{fieldName} {OperandToSql[filter.Operand]} @{parameterKey}";
    }

    public static string GetSortingToSql(SortingDefinition sortingDefinition,
        Dictionary<string, string>? propertyMap = null)
    {
        var fieldName = propertyMap?.TryGetValue(sortingDefinition.FieldName, out var result) ?? false
            ? result
            : sortingDefinition.FieldName;
        return $"{fieldName} {(sortingDefinition.Asc ? "ASC" : "DESC")}";
    }
}