using MediatR;

namespace DumaemSchool.Core.Commands.SMTP;

public sealed record SendRestorationCodeEmailCommand(string Email, int RestorationCode) : IRequest<bool>;