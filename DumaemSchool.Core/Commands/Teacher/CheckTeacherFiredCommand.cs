using MediatR;

namespace DumaemSchool.Core.Commands.Teacher;

public sealed record CheckTeacherFiredCommand(int TeacherId) : IRequest<bool>;