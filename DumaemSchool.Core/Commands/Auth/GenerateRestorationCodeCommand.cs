using LanguageExt.Common;
using MediatR;

namespace DumaemSchool.Core.Commands.Auth;

public sealed record GenerateRestorationCodeCommand(string Email) : IRequest<Result<Guid>>;