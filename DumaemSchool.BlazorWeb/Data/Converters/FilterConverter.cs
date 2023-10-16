using DumaemSchool.Core.DataManipulation;
using MudBlazor;

namespace DumaemSchool.BlazorWeb.Data.Converters;

public static class FilterConverter
{
    private static readonly Dictionary<string, FilterOperand> Operands = new()
    {
        {FilterOperator.String.Contains, FilterOperand.Contains},
        {FilterOperator.String.Equal, FilterOperand.Equal},
        {FilterOperator.String.NotContains, FilterOperand.NotContains},
        {FilterOperator.String.NotEqual, FilterOperand.NotEqual},
        {FilterOperator.String.StartsWith, FilterOperand.StartsWith},
        {FilterOperator.String.EndsWith, FilterOperand.EndsWith},
        {FilterOperator.String.Empty, FilterOperand.IsEmpty},
        {FilterOperator.String.NotEmpty, FilterOperand.IsNotEmpty},
        {FilterOperator.Number.Equal, FilterOperand.Equal},
        {FilterOperator.Number.NotEqual, FilterOperand.NotEqual},
        {FilterOperator.Number.GreaterThan, FilterOperand.GreaterThan},
        {FilterOperator.Number.GreaterThanOrEqual, FilterOperand.GreaterThanOrEqual},
        {FilterOperator.Number.LessThan, FilterOperand.LessThan},
        {FilterOperator.Number.LessThanOrEqual, FilterOperand.LessThanOrEqual},
        {FilterOperator.DateTime.Is, FilterOperand.Equal},
        {FilterOperator.DateTime.IsNot, FilterOperand.NotEqual},
        {FilterOperator.DateTime.After, FilterOperand.GreaterThan},
        {FilterOperator.DateTime.OnOrAfter, FilterOperand.GreaterThanOrEqual},
        {FilterOperator.DateTime.Before, FilterOperand.LessThan},
        {FilterOperator.DateTime.OnOrBefore, FilterOperand.LessThanOrEqual},
    };

    public static IEnumerable<FilterDefinition> Convert<T>(IEnumerable<IFilterDefinition<T>> mudFilters)
    {
        return mudFilters.Where(mudFilter => mudFilter.Column is not null && mudFilter.Operator is not null
                                                                          && mudFilter.Value is not null)
            .Select(mudFilter => new FilterDefinition
            {
                FieldName = mudFilter.Column!.PropertyName,
                Operand = Operands[mudFilter.Operator!],
                Value = mudFilter.Value
            }).ToList();
    }

    public static ListParam GenerateParamsFromGrid<T>(GridState<T> state, List<FilterDefinition>? filters)
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