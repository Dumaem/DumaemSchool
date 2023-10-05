namespace DumaemSchool.Core.DataManipulation;

public sealed class ListParam
{
    public FilterDefinition[] Filters { get; init; } = Array.Empty<FilterDefinition>();
    public SortingDefinition[] Sorting { get; init; } = Array.Empty<SortingDefinition>();
    public PaginationInfo Pagination { get; init; } = new();
}