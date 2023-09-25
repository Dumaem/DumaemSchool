using MediatR;

namespace DumaemSchool.Core.Commands.Auth;

public sealed record RestorationCodeLoginCommand(string Email, Guid SessionId, int CodeProvided) : IRequest<bool>;