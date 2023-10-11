using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Teacher;

public sealed class TeachersQueryHandler : IRequestHandler<TeachersQuery, ListDataResult<TeacherDto>>
{
    private readonly ITeacherRepository _repository;

    public TeachersQueryHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDataResult<TeacherDto>> Handle(TeachersQuery request, 
        CancellationToken cancellationToken)
    {
        return await _repository.ListTeachersAsync( request.Params);
    }
}