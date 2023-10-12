namespace DumaemSchool.Database.DataManipulation;

public sealed class SqlFilterDefinition
{
    public required string SqlText { get; init; } = null!;
    public string? ParameterName { get; init; }
}