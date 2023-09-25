using MediatR;

namespace DumaemSchool.SMTP.Commands;

public sealed record SendRestorationCodeEmailCommand(string Email, int RestorationCode) : IRequest<bool>;