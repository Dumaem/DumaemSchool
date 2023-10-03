using DumaemSchool.Core.Queries;
using DumaemSchool.Database.Repositories;
using MediatR;
using SectionType = DumaemSchool.Core.Models.SectionType;

namespace DumaemSchool.Database.Handlers;

public sealed class SectionTypesQueryHandler : IRequestHandler<SectionTypesQuery, IEnumerable<SectionType>>
{
    private readonly ISectionRepository _repository;

    public SectionTypesQueryHandler(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SectionType>> Handle(SectionTypesQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.ListSectionTypesAsync();
    }
}