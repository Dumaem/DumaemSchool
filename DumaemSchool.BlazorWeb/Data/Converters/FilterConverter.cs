using DumaemSchool.Core.DataManipulation;
using MudBlazor;

namespace DumaemSchool.BlazorWeb.Data.Converters;

public static class FilterConverter<T>
{
    private static readonly Dictionary<string, FilterOperand> Operands = new()
    {
        {"contains", FilterOperand.Contains},
        {"equals", FilterOperand.Equal},
        {"not contains", FilterOperand.NotContains},
        {"not equals", FilterOperand.NotEqual},
        {"starts with", FilterOperand.StartsWith},
        {"ends with", FilterOperand.EndsWith},
        {"is empty", FilterOperand.IsEmpty},
        {"is not empty", FilterOperand.IsNotEmpty},
        {"=", FilterOperand.Equal},
        {"!=", FilterOperand.NotEqual},
        {">", FilterOperand.GreaterThan},
        {">=", FilterOperand.GreaterThanOrEqual},
        {"<", FilterOperand.LessThan},
        {"<=", FilterOperand.LessThanOrEqual},
    };

    public static IEnumerable<FilterDefinition> Convert(IEnumerable<IFilterDefinition<T>> mudFilters)
    {
        return mudFilters.Where(mudFilter => mudFilter.Column is not null && mudFilter.Operator is not null
                                                                          && mudFilter.Value is not null)
            .Select(mudFilter => new FilterDefinition
            {
                FieldName = mudFilter.Column.PropertyName,
                Operand = Operands[mudFilter.Operator],
                Value = mudFilter.Value
            }).ToList();
    }

    public static ListParam GenerateParamsFromGrid(GridState<T> state, List<FilterDefinition>? filters)
    {
        filters ??= new List<FilterDefinition>();
        filters.AddRange(Convert(state.FilterDefinitions));
        return new ListParam
        {
            Pagination = new PaginationInfo {ItemCount = state.PageSize, PageNumber = state.Page},
            Sorting = state.SortDefinitions.Select(x => new SortingDefinition
            {
                FieldName = x.SortBy,
                Asc = !x.Descending
            }).ToArray(),
            Filters = filters.ToArray()
        };
    }
}