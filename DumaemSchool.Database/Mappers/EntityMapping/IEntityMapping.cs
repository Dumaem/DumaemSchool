﻿namespace DumaemSchool.Database.Mappers.EntityMapping;

public interface IEntityMapping<T>
{
    public IReadOnlyList<PropertyMapping> PropertyMappings { get; }
    public IReadOnlyCollection<string> DefaultMappingPropertyNames { get; }
    public IReadOnlyCollection<string> AggregateMappingPropertyNames { get; }
    public Dictionary<string, string> GetMapping();
}