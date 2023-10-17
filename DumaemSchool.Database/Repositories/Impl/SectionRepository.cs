using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.Models;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.ListGetters;
using Microsoft.EntityFrameworkCore;
using DumaemSchool.Database.Mappers;
using Section = DumaemSchool.Core.Models.Section;
using SectionStudent = DumaemSchool.Core.OutputModels.SectionStudent;

namespace DumaemSchool.Database.Repositories.Impl;

public sealed class SectionRepository : ISectionRepository
{
    private readonly DatabaseMapper _mapper;
    private readonly ApplicationContext _context;
    private readonly IListSqlGenerator<SectionInfo> _sectionInfoSqlGenerator;
    private readonly IListSqlGenerator<SectionStudent> _sectionStudentSqlGenerator;
    private readonly IListSqlGenerator<SectionSchedule> _sectionScheduleSqlGenerator;
    private readonly IListSqlGenerator<StudentToAddToSection> _studentToAddSqlGenerator;

    public SectionRepository(IListSqlGenerator<SectionInfo> sectionInfoSqlGenerator,
        ApplicationContext context,
        IListSqlGenerator<SectionStudent> sectionStudentSqlGenerator,
        IListSqlGenerator<SectionSchedule> sectionScheduleSqlGenerator,
        IListSqlGenerator<StudentToAddToSection> studentToAddSqlGenerator)
    {
        _mapper = new DatabaseMapper();
        _sectionInfoSqlGenerator = sectionInfoSqlGenerator;
        _context = context;
        _sectionStudentSqlGenerator = sectionStudentSqlGenerator;
        _sectionScheduleSqlGenerator = sectionScheduleSqlGenerator;
        _studentToAddSqlGenerator = studentToAddSqlGenerator;
    }

    public async Task<ListDataResult<SectionInfo>> ListSectionInfo(ListParam param)
    {
        var listQuery = _sectionInfoSqlGenerator.GetListSql(param);
        var connection = _context.Database.GetDbConnection();
        var result = (await connection
            .QueryAsync<SectionInfo>(listQuery.SelectSql, listQuery.Parameters)
            ).AsList();

        return new ListDataResult<SectionInfo>
        {
            Items = result,
            TotalItemsCount = result.Count
        };
    }

    public async Task<ListDataResult<SectionStudent>> ListSectionStudents(ListParam param)
    {
        var listQuery = _sectionStudentSqlGenerator.GetListSql(param);
        var connection = _context.Database.GetDbConnection();
        var result = (await connection
                .QueryAsync<SectionStudent>(listQuery.SelectSql, listQuery.Parameters)
            ).AsList();

        return new ListDataResult<SectionStudent>
        {
            Items = result,
            TotalItemsCount = result.Count
        };
    }

    public async Task<ListDataResult<SectionSchedule>> ListSectionSchedule(ListParam param)
    {
        var listQuery = _sectionScheduleSqlGenerator.GetListSql(param);
        var connection = _context.Database.GetDbConnection();
        var result = (await connection
                .QueryAsync<SectionSchedule>(listQuery.SelectSql, listQuery.Parameters)
            ).AsList();

        return new ListDataResult<SectionSchedule>
        {
            Items = result,
            TotalItemsCount = result.Count
        };
    }

    public async Task<ListDataResult<StudentToAddToSection>> ListStudentsToAdd(ListParam param)
    {
        var listQuery = _studentToAddSqlGenerator.GetListSql(param);
        var connection = _context.Database.GetDbConnection();
        var result = (await connection
                .QueryAsync<StudentToAddToSection>(listQuery.SelectSql, listQuery.Parameters)
            ).AsList();

        return new ListDataResult<StudentToAddToSection>
        {
            Items = result,
            TotalItemsCount = result.Count
        };
    }

    public async Task<bool> DeleteStudentFromSection(int studentId, int sectionId)
    {
        var sectionStudent = await _context.SectionStudents.FirstOrDefaultAsync(x => x.SectionId == sectionId && x.StudentId == studentId);
        if (sectionStudent is null)
            return false;

        sectionStudent.IsActual = false;
        _context.SectionStudents.Update(sectionStudent);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AddStudentToSection(int studentId, int sectionId)
    {
        var foundStudent = await _context.Students.FindAsync(studentId);
        var foundSection = await _context.Sections.FindAsync(sectionId);

        if (foundStudent is null || foundSection is null) return false;

        var sectionStudent = new Database.Entities.SectionStudent
        {
            StudentId = studentId,
            SectionId = sectionId,
            DateAdded = DateOnly.FromDateTime(DateTime.Now)
        };

        await _context.SectionStudents.AddAsync(sectionStudent);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> CheckStudentAvailabilityToSection(int studentId, List<SectionSchedule> sectionSchedule)
    {
        var sectionTimeRanges = sectionSchedule.Select(x => new TimeRange
        {
            DayOfWeek = x.DayOfWeek,
            TimeStart = x.Time,
            TimeEnd = x.Time.Add(x.Duration),
        });

        var busyTimeRangesForStudent = await (from schedule in _context.Schedules
                                       join sectionStudent in _context.SectionStudents
                                       on schedule.SectionId equals sectionStudent.SectionId
                                       where sectionStudent.IsActual == true && sectionStudent.StudentId == studentId
                                       select new TimeRange
                                       {
                                           DayOfWeek = (DayOfWeek)schedule.DayOfWeek,
                                           TimeStart = schedule.Time,
                                           TimeEnd = schedule.Time.Add(schedule.Duration)
                                       }).ToArrayAsync();

        if (busyTimeRangesForStudent.Any(busyTimeRange => 
        sectionTimeRanges.Any(sectionTimeRange =>
        busyTimeRange.Overlaps(sectionTimeRange))))
        {
            return false;
        }

        return true;
    }

    public async Task<TeacherDto?> GetTeacherFromSection(int sectionId)
    {
        var section = await _context.Sections.FindAsync(sectionId);

        if(section is null)
        {
            return null;
        }    
        var teacher = (await _context.SectionTeachers.Include(x => x.Teacher).FirstOrDefaultAsync(x => x.IsActual == true 
        && x.SectionId == sectionId))!.Teacher;
        return new TeacherDto { Id = teacher.Id, Name = teacher.Name };
    }



    public async Task<Section> CreateSection(SectionWithSchedule section)
    {
        var sectionDb = new Entities.Section
        {
            GroupName = section.GroupName, SectionTypeId = section.SectionTypeId
        };

        var sectionTeacher = new SectionTeacher
        {
            Section = sectionDb, TeacherId = section.TeacherId
        };
        
        var schedules = section.Schedules.Select(x => new Schedule
        {
            Section = sectionDb, 
            Cabinet = x.Cabinet, 
            Duration = x.Duration, 
            Time = x.Time, 
            DayOfWeek = (int)x.DayOfWeek
        });
    
        await _context.Sections.AddAsync(sectionDb);
        await _context.SectionTeachers.AddAsync(sectionTeacher);
        await _context.Schedules.AddRangeAsync(schedules);

        await _context.SaveChangesAsync();

        return _mapper.Map(sectionDb);
    }
}