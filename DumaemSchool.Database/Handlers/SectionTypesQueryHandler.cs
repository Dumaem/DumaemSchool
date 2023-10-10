using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries;
using DumaemSchool.Database.Repositories;
using MediatR;
using SectionType = DumaemSchool.Core.Models.SectionType;

namespace DumaemSchool.Database.Handlers;

public sealed class SectionTypesQueryHandler : IRequestHandler<SectionTypesQuery, ListDataResult<SectionType>>
{
    private readonly ISectionTypeRepository _repository;

    public SectionTypesQueryHandler(ISectionTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDataResult<SectionType>> Handle(SectionTypesQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.ListSectionTypesAsync(request.Params);
    }
}