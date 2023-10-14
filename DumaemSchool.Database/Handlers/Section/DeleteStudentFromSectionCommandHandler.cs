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
    public sealed class DeleteStudentFromSectionCommandHandler : IRequestHandler<DeleteStudentFromSectionCommand, bool>
    {
        private readonly ISectionRepository _repository;

        public DeleteStudentFromSectionCommandHandler(ISectionRepository repository) 
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteStudentFromSectionCommand request,
            CancellationToken token)
        {
            return await _repository.DeleteStudentFromSection(request.StudentId, request.SectionId);
        }
    }
}
