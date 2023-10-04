using MediatR;

namespace DumaemSchool.Core.Commands.Auth;

public sealed record SetUserPasswordCommand(string Email, string NewPassword) : IRequest;