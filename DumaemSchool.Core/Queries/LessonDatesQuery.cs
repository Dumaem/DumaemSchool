using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Queries;

public sealed record LessonDatesQuery(int SectionId) : IRequest<IEnumerable<LessonDate>>;