using System.Diagnostics;
using Dapper;
using DumaemSchool.Core.DataManipulation;

namespace DumaemSchool.Database.DataManipulation;

public static class SqlUtility
{
    private static readonly IReadOnlyCollection<FilterOperand> IntegerOperands = new HashSet<FilterOperand>
    {
        FilterOperand.Equal,
        FilterOperand.NotEqual,
        FilterOperand.GreaterThan,
        FilterOperand.LessThan,
        FilterOperand.GreaterThanOrEqual,
        FilterOperand.LessThanOrEqual,
    };

    private static readonly IReadOnlyCollection<FilterOperand> StringOperands = new HashSet<FilterOperand>
    {
        FilterOperand.Contains,
        FilterOperand.NotContains,
        FilterOperand.Equal,
        FilterOperand.NotEqual,
        FilterOperand.StartsWith,
        FilterOperand.EndsWith,
    };

    public static SqlFilterDefinition? GetFilterToSql(FilterDefinition filter, DynamicParameters parameters,
        IReadOnlyDictionary<string, string>? propertyMap = null)
    {
        var parameterKey = filter.ToString();
        var fieldName = propertyMap?.TryGetValue(filter.FieldName, out var result) ?? false ? result : filter.FieldName;

        if (filter.Operand is FilterOperand.IsEmpty or FilterOperand.IsNotEmpty)
        {
            var addNot = filter.Operand == FilterOperand.IsNotEmpty; 
            return new SqlFilterDefinition
            {
                SqlText = $"{fieldName} is {(addNot ? "NOT" : "")} null",
            };
        }

        var filterValue = filter.Value;
        if (filter.Value?.GetType().IsEnum is true)
        {
            filterValue = (int)filter.Value;
        }

        string? operandString = default;
        switch (filterValue)
        {
            case int or double or DateTime:
            {
                if (!IntegerOperands.Contains(filter.Operand))
                    return null;
                operandString = filter.Operand switch
                {
                    FilterOperand.Equal => "=",
                    FilterOperand.NotEqual => "<>",
                    FilterOperand.GreaterThan => ">",
                    FilterOperand.LessThan => "<",
                    FilterOperand.GreaterThanOrEqual => ">=",
                    FilterOperand.LessThanOrEqual => "<=",
                    _ => throw new UnreachableException()
                };

                parameters.Add(parameterKey, filter.Value);
                return new SqlFilterDefinition
                {
                    SqlText = $"{fieldName} {operandString} @{parameterKey}",
                    ParameterName = parameterKey
                };
            }
            case string stringValue:
                if (!StringOperands.Contains(filter.Operand))
                    return null;

                switch (filter.Operand)
                {
                    case FilterOperand.StartsWith:
                        operandString = "ILIKE";
                        stringValue = $"{stringValue}%";
                        break;
                    case FilterOperand.EndsWith:
                        operandString = "ILIKE";
                        stringValue = $"%{stringValue}";
                        break;
                    case FilterOperand.Contains:
                        operandString = "ILIKE";
                        stringValue = $"%{stringValue}%";
                        break;
                    case FilterOperand.NotContains:
                        operandString = "NOT ILIKE";
                        stringValue = $"%{stringValue}%";
                        break;
                    case FilterOperand.Equal:
                        operandString = "=";
                        break;
                    case FilterOperand.NotEqual:
                        operandString = "<>";
                        break;
                }

                parameters.Add(parameterKey, stringValue);
                return new SqlFilterDefinition
                {
                    SqlText = $"{fieldName} {operandString} @{parameterKey}",
                    ParameterName = parameterKey
                };
            case bool boolValue:
                if (filter.Operand != FilterOperand.Equal)
                    return null;

                return new SqlFilterDefinition
                {
                    SqlText = $"{(!boolValue ? "NOT" : "")} {fieldName}"
                };
            default:
                return null;
        }
    }

    public static string GetSortingToSql(SortingDefinition sortingDefinition,
        IReadOnlyDictionary<string, string>? propertyMap = null)
    {
        var fieldName = propertyMap?.TryGetValue(sortingDefinition.FieldName, out var result) ?? false
            ? result
            : sortingDefinition.FieldName;
        return $"{fieldName} {(sortingDefinition.Asc ? "ASC" : "DESC")}";
    }
}