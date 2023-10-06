namespace DumaemSchool.Database.Mappers.EntityMapping;

public abstract class AbstractEntityMapping<TEntity> : IEntityMapping<TEntity>
{
    private List<PropertyMapping> Mappings { get; } = new();

    public IReadOnlyList<PropertyMapping> PropertyMappings => Mappings.AsReadOnly();

    private readonly HashSet<string> _defaultPropertyMappings = new();
    public IReadOnlyCollection<string> DefaultMappingPropertyNames => _defaultPropertyMappings;

    private readonly HashSet<string> _aggregatePropertyMappings = new();
    public IReadOnlyCollection<string> AggregateMappingPropertyNames => _aggregatePropertyMappings;

    protected void Map(string propertyName, string databaseExpression, bool isAggregate = false)
    {
        var mapping = new PropertyMapping
        {
            PropertyName = propertyName,
            SqlExpression = databaseExpression,
            IsAggregate = isAggregate
        };
        Mappings.Add(mapping);
        if (isAggregate)
            _aggregatePropertyMappings.Add(propertyName);
        else
            _defaultPropertyMappings.Add(propertyName);
    }

    public Dictionary<string, string> GetMapping()
    {
        return Mappings
            .OrderBy(x => x.IsAggregate)
            .ToDictionary(x => x.PropertyName, x => x.SqlExpression);
    }
}