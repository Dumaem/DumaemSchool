using DumaemSchool.Core.Commands;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers;

public sealed class AddSectionTypeCommandHandler : IRequestHandler<AddSectionTypeCommand, int>
{
    private readonly ISectionRepository _repository;

    public AddSectionTypeCommandHandler(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<int> Handle(AddSectionTypeCommand request, 
        CancellationToken cancellationToken)
    {
        return await _repository.AddSectionTypeAsync(request.SectionTypeName);
    }
}