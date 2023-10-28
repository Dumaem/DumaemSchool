using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.Models;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.ListGetters;
using DumaemSchool.Database.Mappers;
using Microsoft.EntityFrameworkCore;
using Teacher = DumaemSchool.Core.Models.Teacher;

namespace DumaemSchool.Database.Repositories.Impl;

public sealed class TeacherRepository : ITeacherRepository
{
    private readonly ApplicationContext _context;
    private readonly DatabaseMapper _mapper;
    private readonly IListSqlGenerator<TeacherDto> _sqlGenerator;

    public TeacherRepository(ApplicationContext context,
        IListSqlGenerator<TeacherDto> sqlGenerator)
    {
        _context = context;
        _sqlGenerator = sqlGenerator;
        _mapper = new DatabaseMapper();
    }

    public async Task<ListDataResult<TeacherDto>> ListTeachersAsync(ListParam param)
    {
        var listQuery = _sqlGenerator.GetListSql(param);
        var connection = _context.Database.GetDbConnection();
        var result = await connection
            .QueryAsync<TeacherDto>(listQuery.SelectSql, listQuery.Parameters);
        var count = await connection.ExecuteScalarAsync<int>(listQuery.CountSql, listQuery.Parameters);

        return new ListDataResult<TeacherDto>
        {
            Items = result,
            TotalItemsCount = count
        };
    }

    public async Task<Teacher> AddTeacherAsync(Teacher teacher)
    {
        var teacherDb = _mapper.Map(teacher);
        await _context.Teachers.AddAsync(teacherDb);
        await _context.SaveChangesAsync();
        return _mapper.Map(teacherDb);
    }

    public async Task<Teacher?> GetTeacherInfo(int teacherId)
    {
        var teacherDb = await _context.Teachers.FindAsync(teacherId);
        if (teacherDb is null)
            return null;

        var teacher = _mapper.Map(teacherDb);
        teacher.Email = _context.Database.GetDbConnection().ExecuteScalar<string>(@"SELECT u.""Email"" FROM identity.""AspNetUsers"" u JOIN identity.""AspNetUserClaims"" uc ON u.""Id"" = uc.""UserId"" WHERE uc.""ClaimValue"" = @teacherId", new { teacherId = teacherId.ToString() });
        return teacher;
    }

    public async Task<bool> UpdateTeacherNameAsync(int teacherId, string name)
    {
        var teacherDb = await _context.Teachers.FindAsync(teacherId);
        if (teacherDb is null)
            return false;

        teacherDb.Name = name;
        _context.Update(teacherDb);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTeacherAsync(int id)
    {
        var teacher = await _context.Teachers.FindAsync(id);
        if (teacher is null)
            return false;

        var deleteDate = DateOnly.FromDateTime(DateTime.Now);
        teacher.IsDeleted = true;
        teacher.DateDeleted = deleteDate;
        _context.Teachers.Update(teacher);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CheckTeacherAvailabilityToSection(int teacherId, List<SectionSchedule> sectionSchedule)
    {
        var sectionTimeRanges = sectionSchedule.Select(x => new TimeRange
        {
            DayOfWeek = x.DayOfWeek,
            TimeStart = x.Time,
            TimeEnd = x.Time.Add(x.Duration),
        });

        var busyTimeRanges = await (from schedule in _context.Schedules
                                              join sectionTeacher in _context.SectionTeachers
                                              on schedule.SectionId equals sectionTeacher.SectionId
                                              where sectionTeacher.IsActual == true && sectionTeacher.TeacherId == teacherId
                                              select new TimeRange
                                              {
                                                  DayOfWeek = (DayOfWeek)schedule.DayOfWeek,
                                                  TimeStart = schedule.Time,
                                                  TimeEnd = schedule.Time.Add(schedule.Duration)
                                              }).ToArrayAsync();

        return !busyTimeRanges.Any(busyTimeRange =>
            sectionTimeRanges.Any(sectionTimeRange =>
                busyTimeRange.Overlaps(sectionTimeRange)));
    }
 
    public async Task<bool> AddTeacherToSection(int teacherId, int sectionId)
    {
        var foundTeacher = await _context.Teachers.FindAsync(teacherId);
        var foundSection = await _context.Sections.FindAsync(sectionId);
        var foundOldSectionTeacherRecord = await _context.SectionTeachers
                                    .FirstOrDefaultAsync(x => x.IsActual == true && x.SectionId == sectionId);

        if (foundTeacher is null || foundSection is null || foundOldSectionTeacherRecord is null) return false;

        var sectionTeacher = new SectionTeacher
        {
            TeacherId = teacherId,
            SectionId = sectionId,
        };

        foundOldSectionTeacherRecord!.IsActual = false;

        _context.SectionTeachers.Update(foundOldSectionTeacherRecord);
        await _context.SectionTeachers.AddAsync(sectionTeacher);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        return true;
    }
}