using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Queries.Statistics;

public sealed record SectionStudentsStatisticsQuery(int SectionId) : IRequest<ListDataResult<SectionStudentStatistics>>;