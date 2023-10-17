using DumaemSchool.Core.Commands.Section;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Section
{
    public sealed class CheckStudentAvailabilityToSectionCommandHandler : IRequestHandler<CheckStudentAvailabilityToSectionCommand, bool>
    {
        private readonly ISectionRepository _repository;

        public CheckStudentAvailabilityToSectionCommandHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CheckStudentAvailabilityToSectionCommand request,
        CancellationToken token)
        {
            return await _repository.CheckStudentAvailabilityToSection(request.StudentId, request.SectionSchedule);
        }
    }
}
