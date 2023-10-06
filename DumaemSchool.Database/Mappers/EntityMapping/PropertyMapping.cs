namespace DumaemSchool.Database.Mappers.EntityMapping;

public sealed class PropertyMapping
{
    public required string PropertyName { get; init; }
    public required string SqlExpression { get; init; }
    public bool IsAggregate { get; init; }
}