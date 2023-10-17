using DumaemSchool.Core.OutputModels;
using DumaemSchool.Core.Queries.Lesson;
using DumaemSchool.Database.Repositories;
using MediatR;

namespace DumaemSchool.Database.Handlers.Lesson;

public sealed class
    TeacherLessonScheduleQueryHandler : IRequestHandler<TeacherLessonScheduleQuery, ListDataResult<LessonForScheduler>>
{
    private readonly ILessonRepository _repository;

    public TeacherLessonScheduleQueryHandler(ILessonRepository repository)
    {
        _repository = repository;
    }

    public async Task<ListDataResult<LessonForScheduler>> Handle(TeacherLessonScheduleQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.ListTeacherLessonSchedule(request.Param);
    }
}