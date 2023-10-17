using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.ListGetters;
using DumaemSchool.Database.Mappers;
using Microsoft.EntityFrameworkCore;
using Lesson = DumaemSchool.Core.Models.Lesson;

namespace DumaemSchool.Database.Repositories.Impl;

public sealed class LessonRepository : ILessonRepository
{
    private readonly ApplicationContext _context;
    private readonly DatabaseMapper _mapper;
    private readonly IListSqlGenerator<StudentLessonStatistics> _statisticsListSqlGenerator;
    private readonly IListSqlGenerator<LessonForScheduler> _lessonScheduleListSqlGenerator;

    public LessonRepository(ApplicationContext context, 
        IListSqlGenerator<StudentLessonStatistics> statisticsListSqlGenerator, 
        IListSqlGenerator<LessonForScheduler> lessonScheduleListSqlGenerator)
    {
        _context = context;
        _statisticsListSqlGenerator = statisticsListSqlGenerator;
        _lessonScheduleListSqlGenerator = lessonScheduleListSqlGenerator;
        _mapper = new DatabaseMapper();
    }

    public Task<IEnumerable<LessonDate>> ListSectionLessonDates(int sectionId)
    {
        return Task.FromResult<IEnumerable<LessonDate>>(_context.Lessons
            .Where(x => x.Schedule.SectionId == sectionId)
            .Select(x => new LessonDate { ConductionDate = x.Date, LessonId = x.Id }));
    }

    public async Task<ListDataResult<StudentLessonStatistics>> ListLessonStatistics(ListParam param)
    {
        var listQuery = _statisticsListSqlGenerator.GetListSql(param);
        var connection = _context.Database.GetDbConnection();
        var result = (await connection
                .QueryAsync<StudentLessonStatistics>(listQuery.SelectSql, listQuery.Parameters)
            ).AsList();

        return new ListDataResult<StudentLessonStatistics>
        {
            Items = result, TotalItemsCount = result.Count
        };
    }

    public async Task<Lesson> CreateLesson(Lesson lesson)
    {
        var lessonDb = _mapper.Map(lesson);
        await _context.Lessons.AddAsync(lessonDb);
        await _context.SaveChangesAsync();
        return _mapper.Map(lessonDb);
    }

    public async Task<ListDataResult<LessonForScheduler>> ListTeacherLessonSchedule(ListParam param)
    {
        var listQuery = _lessonScheduleListSqlGenerator.GetListSql(param);
        var connection = _context.Database.GetDbConnection();
        var result = (await connection
                .QueryAsync<LessonForScheduler>(listQuery.SelectSql, listQuery.Parameters)
            ).AsList();

        return new ListDataResult<LessonForScheduler>
        {
            Items = result, TotalItemsCount = result.Count
        };
    }
}