using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.ListGetters;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Database.Repositories.Impl;

public sealed class LessonRepository : ILessonRepository
{
    private readonly ApplicationContext _context;
    private readonly IListSqlGenerator<StudentLessonStatistics> _statisticsListSqlGenerator;

    public LessonRepository(ApplicationContext context, 
        IListSqlGenerator<StudentLessonStatistics> statisticsListSqlGenerator)
    {
        _context = context;
        _statisticsListSqlGenerator = statisticsListSqlGenerator;
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
}