namespace DumaemSchool.Core.DataManipulation;

public sealed class ListParam
{
    public FilterDefinition[] Filters { get; set; } = Array.Empty<FilterDefinition>();
    public SortingDefinition[] Sorting { get; set; } = Array.Empty<SortingDefinition>();
    public PaginationInfo Pagination { get; set; } = new();
}