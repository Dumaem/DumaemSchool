using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries.Statistics;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Statistics;

public sealed class
    SectionStudentsStatisticsQueryHandler : IRequestHandler<SectionStudentsStatisticsQuery,
        ListDataResult<SectionStudentStatistics>>
{
    private readonly ISectionRepository _repository;

    public SectionStudentsStatisticsQueryHandler(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDataResult<SectionStudentStatistics>> Handle(SectionStudentsStatisticsQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.ListSectionStudentsStatistics(request.SectionId);
    }
}