namespace DumaemSchool.Core.OutputModels;

public sealed class ListDataResult<T>
{
    public required IEnumerable<T> Items { get; init; }
    public required int TotalItemsCount { get; init; }
}