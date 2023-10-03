using DumaemSchool.Core.Commands;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers;

public sealed class DeleteSectionTypeCommandHandler : IRequestHandler<DeleteSectionTypeCommand, bool>
{
    private readonly ISectionRepository _repository;

    public DeleteSectionTypeCommandHandler(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(DeleteSectionTypeCommand request, 
        CancellationToken cancellationToken)
    {
        return await _repository.DeleteSectionTypeAsync(request.SectionTypeId);
    }
}