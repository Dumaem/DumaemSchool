using DumaemSchool.Core.Commands;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers;

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