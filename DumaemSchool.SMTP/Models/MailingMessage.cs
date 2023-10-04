namespace DumaemSchool.SMTP.Models;

public sealed class MailingMessage
{
    public required string ToEmail { get; init; }
    public required string Subject { get; init; }
    public required string Body { get; init; }
}