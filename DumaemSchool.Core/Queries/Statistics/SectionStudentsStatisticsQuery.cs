using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Queries.Statistics;

public sealed record SectionStudentsStatisticsQuery(ListParam Param) : IRequest<ListDataResult<SectionStudentStatistics>>;