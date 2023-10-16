using DumaemSchool.Core.Commands.Section;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Section;

public sealed class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Core.Models.Section>
{
    private readonly ISectionRepository _repository;

    public CreateSectionCommandHandler(ISectionRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Core.Models.Section> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
    {
        return await _repository.CreateSection(request.Section);
    }
}