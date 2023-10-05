namespace DumaemSchool.Core.DataManipulation;

public sealed class SortingDefinition
{
    public required string FieldName { get; set; }

    public bool Asc { get; set; } = true;

    public string ToSql()
    {
        return $"";
    }
}