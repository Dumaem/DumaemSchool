using DumaemSchool.Core.Commands;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers;

public sealed class UpdateSectionTypeNameCommandHandler : IRequestHandler<UpdateSectionTypeNameCommand, bool>
{
    private readonly ISectionRepository _repository;

    public UpdateSectionTypeNameCommandHandler(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> Handle(UpdateSectionTypeNameCommand request, 
        CancellationToken cancellationToken)
    {
        if (request.SectionType.Id == default)
            return false;

        return await _repository.UpdateSectionTypeNameAsync(request.SectionType.Id, request.SectionType.Name);
    }
}