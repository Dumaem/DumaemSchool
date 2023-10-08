namespace DumaemSchool.Core.DataManipulation;

public sealed class SortingDefinition
{
    public required string FieldName { get; init; }

    public bool Asc { get; init; } = true;
}