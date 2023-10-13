using DumaemSchool.Core.Commands.Section;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries.Section;
using DumaemSchool.Database.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DumaemSchool.Database.Handlers.Section
{
    public sealed class GetTeacherFromSectionQueryHandler : IRequestHandler<GetTeacherFromSectionQuery, TeacherDto>
    {
        private readonly ISectionRepository _repository;

        public GetTeacherFromSectionQueryHandler(ISectionRepository repository)
        {
            _repository = repository;
        }

        public async Task<TeacherDto> Handle(GetTeacherFromSectionQuery request,
            CancellationToken cancellationToken)
        {
            return await _repository.GetTeacherFromSection(request.SectionId);
        }
    }
}
