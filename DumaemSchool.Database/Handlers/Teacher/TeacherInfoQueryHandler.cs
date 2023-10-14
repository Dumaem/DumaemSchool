using DumaemSchool.Core.Queries.Teacher;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Teacher;

public sealed class TeacherInfoQueryHandler : IRequestHandler<TeacherInfoQuery, Core.Models.Teacher?>
{
    private readonly ITeacherRepository _repository;

    public TeacherInfoQueryHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<Core.Models.Teacher?> Handle(TeacherInfoQuery request, 
        CancellationToken cancellationToken)
    {
        return await _repository.GetTeacherInfo(request.TeacherId);
    }
}