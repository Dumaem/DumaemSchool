using DumaemSchool.Core.Queries;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers;

public sealed class TeachersQueryHandler : IRequestHandler<TeachersQuery, IEnumerable<Core.Models.Teacher>>
{
    private readonly ITeacherRepository _repository;

    public TeachersQueryHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Core.Models.Teacher>> Handle(TeachersQuery request, 
        CancellationToken cancellationToken)
    {
        return await _repository.ListTeachersAsync(request.IncludeFired, request.Params);
    }
}