using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries.Section;
using DumaemSchool.Database.Repositories;
using MediatR;

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
