using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers;

public sealed class SectionsInfoQueryHandler : IRequestHandler<SectionsInfoQuery, ListDataResult<SectionInfo>>
{
    private readonly ISectionRepository _repository;

    public SectionsInfoQueryHandler(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDataResult<SectionInfo>> Handle(SectionsInfoQuery request, 
        CancellationToken cancellationToken)
    {
        return await _repository.ListSectionInfo(request.Param);
    }
}