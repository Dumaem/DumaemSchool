using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers;

public sealed class SectionScheduleQueryHandler : IRequestHandler<SectionScheduleQuery, ListDataResult<SectionSchedule>>
{
    private readonly ISectionRepository _repository;

    public SectionScheduleQueryHandler(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDataResult<SectionSchedule>> Handle(SectionScheduleQuery request, 
        CancellationToken cancellationToken)
    {
        return await _repository.ListSectionSchedule(request.Param);
    }
}