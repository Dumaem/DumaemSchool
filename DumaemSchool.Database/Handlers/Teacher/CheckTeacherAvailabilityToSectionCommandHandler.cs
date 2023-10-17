using DumaemSchool.Core.Commands.Teacher;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Teacher
{
    public sealed class CheckTeacherAvailabilityToSectionCommandHandler : IRequestHandler<CheckTeacherAvailabilityToSectionCommand, bool>
    {
        private readonly ITeacherRepository _repository;

        public CheckTeacherAvailabilityToSectionCommandHandler(ITeacherRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CheckTeacherAvailabilityToSectionCommand request,
        CancellationToken token)
        {
            return await _repository.CheckTeacherAvailabilityToSection(request.TeacherId, request.SectionSchedule);
        }
    }
}
