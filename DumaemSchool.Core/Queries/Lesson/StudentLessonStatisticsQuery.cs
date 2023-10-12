using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Queries.Lesson;

public sealed record StudentLessonStatisticsQuery(ListParam Param) : IRequest<ListDataResult<StudentLessonStatistics>>;