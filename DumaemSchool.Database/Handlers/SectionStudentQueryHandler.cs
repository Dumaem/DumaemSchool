using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries;
using DumaemSchool.Database.Repositories;
using MediatR;
using SectionStudent = DumaemSchool.Core.OutputModels.SectionStudent;

namespace DumaemSchool.Database.Handlers;

public sealed class SectionStudentQueryHandler : IRequestHandler<SectionStudentQuery, ListDataResult<SectionStudent>>
{
    private readonly ISectionRepository _repository;

    public SectionStudentQueryHandler(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDataResult<SectionStudent>> Handle(SectionStudentQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.ListSectionStudents(request.Param);
    }
}