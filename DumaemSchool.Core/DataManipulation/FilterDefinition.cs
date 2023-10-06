namespace DumaemSchool.Core.DataManipulation;

public sealed class FilterDefinition
{
    public required string FieldName { get; init; }
    public required FilterOperand Operand { get; init; }
    public object? Value { get; init; }

    public override string ToString()
    {
        return $"{FieldName}_{Operand}";
    }
}