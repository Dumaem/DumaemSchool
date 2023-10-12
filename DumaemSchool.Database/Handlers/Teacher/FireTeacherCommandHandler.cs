using DumaemSchool.Core.Commands.Teacher;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Teacher;

public sealed class FireTeacherCommandHandler : IRequestHandler<FireTeacherCommand, bool>
{
    private readonly ITeacherRepository _repository;

    public FireTeacherCommandHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(FireTeacherCommand request, 
        CancellationToken cancellationToken)
    {
        return await _repository.DeleteTeacherAsync(request.TeacherId);
    }
}