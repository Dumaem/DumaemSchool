using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries.Section;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Section;

public sealed class
    SectionStudentsToAddQueryHandler : IRequestHandler<SectionStudentsToAddQuery, ListDataResult<StudentToAddToSection>>
{
    private readonly ISectionRepository _repository;

    public SectionStudentsToAddQueryHandler(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDataResult<StudentToAddToSection>> Handle(SectionStudentsToAddQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.ListStudentsToAdd(request.Param);
    }
}