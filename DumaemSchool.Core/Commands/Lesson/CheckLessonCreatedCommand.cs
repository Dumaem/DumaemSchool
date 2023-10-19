using MediatR;

namespace DumaemSchool.Core.Commands.Lesson;

public sealed record CheckLessonCreatedCommand(int ScheduleId, DateOnly CreationDate) : IRequest<bool>;