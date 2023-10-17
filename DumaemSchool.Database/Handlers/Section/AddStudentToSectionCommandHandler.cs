using DumaemSchool.Core.Commands.Section;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Section
{
    public sealed class AddStudentToSectionCommandHandler : IRequestHandler<AddStudentToSectionCommand, bool>
    {
        private readonly ISectionRepository _repository;

        public AddStudentToSectionCommandHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AddStudentToSectionCommand request,
        CancellationToken token)
        {
            return await _repository.AddStudentToSection(request.StudentId, request.SectionId);
        }
    }
}
