using MediatR;

namespace DumaemSchool.Core.Queries.Lesson;

public sealed record LessonInfoQuery(int LessonId) : IRequest<Models.Lesson?>;