using DumaemSchool.Core.Commands.Section;
using DumaemSchool.Core.Commands.Teacher;
using DumaemSchool.Database.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumaemSchool.Database.Handlers.Teacher
{
    public sealed class AddTeacherToSectionCommandHandler : IRequestHandler<AddTeacherToSectionCommand, bool>
    {
        private readonly ITeacherRepository _repository;

        public AddTeacherToSectionCommandHandler(ITeacherRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(AddTeacherToSectionCommand request,
        CancellationToken token)
        {
            return await _repository.AddTeacherToSection(request.TeacherId, request.SectionId);
        }
    }
}
