using DumaemSchool.Core.Commands;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers;

public sealed class UpdateSectionTypeNameCommandHandler : IRequestHandler<UpdateSectionTypeNameCommand, bool>
{
    private readonly ISectionTypeRepository _repository;

    public UpdateSectionTypeNameCommandHandler(ISectionTypeRepository repository)
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