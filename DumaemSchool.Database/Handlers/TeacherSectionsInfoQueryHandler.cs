using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers;

public sealed class TeacherSectionsInfoQueryHandler : IRequestHandler<TeacherSectionsInfoQuery, ListDataResult<SectionInfo>>
{
    private readonly ISectionRepository _repository;

    public TeacherSectionsInfoQueryHandler(ISectionRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDataResult<SectionInfo>> Handle(TeacherSectionsInfoQuery request, 
        CancellationToken cancellationToken)
    {
        return await _repository.ListSectionInfo(request.Param);
    }
}