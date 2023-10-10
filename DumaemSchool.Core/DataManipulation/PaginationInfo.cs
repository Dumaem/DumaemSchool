namespace DumaemSchool.Core.DataManipulation;

public class PaginationInfo
{
    public int PageNumber { get; init; }
    public int ItemCount { get; init; }

    public bool IsEmpty => PageNumber == default && ItemCount == default;
}