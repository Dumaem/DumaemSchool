using MediatR;

namespace DumaemSchool.Core.Commands.Lesson;

public sealed record CheckLessonTeacherCommand(int TeacherId, DateOnly Date) : IRequest<bool>;
