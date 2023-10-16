using DumaemSchool.Core.Commands.Section;
using DumaemSchool.Database.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
