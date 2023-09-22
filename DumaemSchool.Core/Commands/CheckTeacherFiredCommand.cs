using MediatR;

namespace DumaemSchool.Core.Commands;

public sealed record CheckTeacherFiredCommand(int TeacherId) : IRequest<bool>;