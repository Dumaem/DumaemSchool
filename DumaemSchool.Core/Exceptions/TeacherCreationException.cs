namespace DumaemSchool.Core.Exceptions;

public sealed class TeacherCreationException : InformationException
{
    public TeacherCreationException(string? message) : base(message)
    {
    }
}