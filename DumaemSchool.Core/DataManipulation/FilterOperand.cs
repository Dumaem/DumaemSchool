namespace DumaemSchool.Core.DataManipulation;

public enum FilterOperand
{
    /// <summary>
    /// Равно
    /// </summary>
    Equal,

    /// <summary>
    /// Не равно
    /// </summary>
    NotEqual,

    /// <summary>
    /// Больше чем
    /// </summary>
    GreaterThan,

    /// <summary>
    /// Меньше чем
    /// </summary>
    LessThan,

    /// <summary>
    /// Больше или равно
    /// </summary>
    GreaterThanOrEqual,

    /// <summary>
    /// Меньше или равно
    /// </summary>
    LessThanOrEqual,

    /// <summary>
    /// Содержит
    /// </summary>
    Contains,
    
    /// <summary>
    /// Не содержит
    /// </summary>
    NotContains,
    
    /// <summary>
    /// Начинается на
    /// </summary>
    StartsWith,
    
    /// <summary>
    /// Заканчивается на
    /// </summary>
    EndsWith,
    
    /// <summary>
    /// Пустой
    /// </summary>
    IsEmpty,
    
    /// <summary>
    /// Не пустой
    /// </summary>
    IsNotEmpty,
}