using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.Models;
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

    public async Task ChangeLessonStudentActivity(int lessonId, int studentId, LessonActivityMark mark)
    {
        var activity = await _context.Activities.FirstOrDefaultAsync(x => x.LessonId == lessonId
                                                                          && x.StudentId == studentId);
        if (activity is not null)
        {
            activity.Mark = (int)mark;
            _context.Update(activity);
        }
        else
        {
            _context.Activities.Add(new Activity
            {
                StudentId = studentId,
                LessonId = lessonId,
                Mark = (int)mark
            });
        }

        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
    }

    public async Task ChangeLessonStudentAttendance(int lessonId, int studentId, bool wasAttended)
    {
        var attendance =
            await _context.Attendances.FirstOrDefaultAsync(x => x.LessonId == lessonId && x.StudentId == studentId);
        if (!wasAttended)
        {
            if (attendance is not null)
                return;

            _context.Attendances.Add(new Attendance
            {
                LessonId = lessonId,
                StudentId = studentId
            });
        }
        else
        {
            if (attendance is null)
                return;
            _context.Remove(attendance!);
        }
        
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
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