namespace DumaemSchool.Core.Exceptions;

/// <summary>
/// Исключение, несущее информативный характер, не должно мешать работе приложения
/// </summary>
public class InformationException : Exception
{
    public InformationException(string? message) : base(message)
    {
    }
}