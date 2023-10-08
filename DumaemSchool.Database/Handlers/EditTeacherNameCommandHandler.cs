using DumaemSchool.Core.Commands;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers;

public sealed class EditTeacherNameCommandHandler : IRequestHandler<EditTeacherNameCommand, bool>
{
    private readonly ITeacherRepository _repository;

    public EditTeacherNameCommandHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(EditTeacherNameCommand request, 
        CancellationToken cancellationToken)
    {
        if (request.Teacher.Id == default)
            return false;

        return await _repository.UpdateTeacherNameAsync(request.Teacher.Id, request.Teacher.Name);
    }
}