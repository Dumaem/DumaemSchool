namespace DumaemSchool.Database.Mappers.EntityMapping.Base;

public sealed class PropertyMapping
{
    public required string PropertyName { get; init; }
    public required string SqlExpression { get; init; }
    public bool IsPrimaryKey { get; init; }
    public bool IsAggregate { get; init; }
}