using DumaemSchool.Core.DataManipulation;

namespace DumaemSchool.Database.Mappers;

public static class SqlUtility
{
    private static readonly Dictionary<FilterOperand, string> OperandToSql = new()
    {
        { FilterOperand.Contains, "ILIKE" },
        { FilterOperand.Equal, "=" },
        { FilterOperand.NotEqual, "<>" },
        { FilterOperand.GreaterThan, ">" },
        { FilterOperand.LessThan, "<" },
        { FilterOperand.GreaterThanOrEqual, ">=" },
        { FilterOperand.LessThanOrEqual, "<=" },
    };

    public static string GetFilterToSql(FilterDefinition filter, Dictionary<string, string>? propertyMap = null)
    {
        var fieldName = propertyMap?[filter.FieldName] ?? filter.FieldName;
        var value = filter.Value is string ? filter.Operand is FilterOperand.Contains ? $"'%{filter.Value}%'" : $"'{filter.Value}'" : $"{filter.Value}";

        return $"{fieldName} {OperandToSql[filter.Operand]} {value}";
    }

    public static string GetSortingToSql(SortingDefinition sortingDefinition, Dictionary<string, string>? propertyMap = null)
    {
        var fieldName = propertyMap?[sortingDefinition.FieldName] ?? sortingDefinition.FieldName;
        return $"{fieldName} {(sortingDefinition.Asc ? "ASC" : "DESC")}";
    }
}