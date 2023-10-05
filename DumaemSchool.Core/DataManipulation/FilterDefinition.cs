namespace DumaemSchool.Core.DataManipulation;

public sealed class FilterDefinition
{
    public required string FieldName { get; set; }
    public required FilterOperand Operand { get; set; }
    public object? Value { get; set; }
}