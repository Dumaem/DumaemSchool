using DumaemSchool.Core.Queries;
using DumaemSchool.Database.Repositories;
using MediatR;
using Teacher = DumaemSchool.Core.Models.Teacher;

namespace DumaemSchool.Database.Handlers;

public sealed class TeacherInfoQueryHandler : IRequestHandler<TeacherInfoQuery, Teacher?>
{
    private readonly ITeacherRepository _repository;

    public TeacherInfoQueryHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<Teacher?> Handle(TeacherInfoQuery request, 
        CancellationToken cancellationToken)
    {
        return await _repository.GetTeacherInfo(request.TeacherId);
    }
}