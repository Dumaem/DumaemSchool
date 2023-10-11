using MediatR;

namespace DumaemSchool.Core.Commands.Teacher;

public sealed record FireTeacherCommand(int TeacherId) : IRequest<bool>;