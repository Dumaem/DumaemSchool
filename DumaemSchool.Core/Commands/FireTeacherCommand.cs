using MediatR;

namespace DumaemSchool.Core.Commands;

public sealed record FireTeacherCommand(int TeacherId) : IRequest<bool>;