using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Queries;

public sealed record SectionScheduleQuery(ListParam Param) : IRequest<ListDataResult<SectionSchedule>>;