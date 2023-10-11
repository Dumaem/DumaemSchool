using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.SectionType;

public sealed class SectionTypesQueryHandler : IRequestHandler<SectionTypesQuery, ListDataResult<Core.Models.SectionType>>
{
    private readonly ISectionTypeRepository _repository;

    public SectionTypesQueryHandler(ISectionTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDataResult<Core.Models.SectionType>> Handle(SectionTypesQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.ListSectionTypesAsync(request.Params);
    }
}