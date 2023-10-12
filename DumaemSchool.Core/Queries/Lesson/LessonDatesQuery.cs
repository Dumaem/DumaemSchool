using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Queries.Lesson;

public sealed record LessonDatesQuery(int SectionId) : IRequest<IEnumerable<LessonDate>>;