using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.ListGetters;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using SectionStudent = DumaemSchool.Core.OutputModels.SectionStudent;

namespace DumaemSchool.Database.Repositories.Impl;

public sealed class SectionRepository : ISectionRepository
{
    private readonly ApplicationContext _context;
    private readonly IListSqlGenerator<SectionInfo> _sectionInfoSqlGenerator;
    private readonly IListSqlGenerator<SectionStudent> _sectionStudentSqlGenerator;
    private readonly IListSqlGenerator<SectionSchedule> _sectionScheduleSqlGenerator;
    private readonly IListSqlGenerator<StudentToAddToSection> _studentToAddSqlGenerator;
    private readonly IListSqlGenerator<SectionStudentStatistics> _sectionStudentStatisticsSqlGenerator;

    public SectionRepository(IListSqlGenerator<SectionInfo> sectionInfoSqlGenerator,
        ApplicationContext context,
        IListSqlGenerator<SectionStudent> sectionStudentSqlGenerator,
        IListSqlGenerator<SectionSchedule> sectionScheduleSqlGenerator,
        IListSqlGenerator<StudentToAddToSection> studentToAddSqlGenerator,
        IListSqlGenerator<SectionStudentStatistics> sectionStudentStatisticsSqlGenerator)
    {
        _sectionInfoSqlGenerator = sectionInfoSqlGenerator;
        _context = context;
        _sectionStudentSqlGenerator = sectionStudentSqlGenerator;
        _sectionScheduleSqlGenerator = sectionScheduleSqlGenerator;
        _studentToAddSqlGenerator = studentToAddSqlGenerator;
        _sectionStudentStatisticsSqlGenerator = sectionStudentStatisticsSqlGenerator;
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

    public async Task<ListDataResult<SectionStudentStatistics>> ListSectionStudentsStatistics(int sectionId)
    {
        var param = new ListParam
        {
            Filters = new[]
            {
                new FilterDefinition
                {
                    FieldName = nameof(SectionStudentStatistics.SectionId), Operand = FilterOperand.Equal,
                    Value = sectionId
                }
            }
        };

        var listQuery = _sectionScheduleSqlGenerator.GetListSql(param);
        var connection = _context.Database.GetDbConnection();
        var result = (await connection
                .QueryAsync<SectionStudentStatistics>(listQuery.SelectSql, listQuery.Parameters)
            ).AsList();

        return new ListDataResult<SectionStudentStatistics>
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
            Items = result, TotalItemsCount = result.Count
        };
    }

    public async Task<bool> DeleteStudentFromSection(int studentId, int sectionId)
    {
        var sectionStudent =
            await _context.SectionStudents.FirstOrDefaultAsync(
                x => x.SectionId == sectionId && x.StudentId == studentId);
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

        var sectionStudent = new SectionStudent
        {
            StudentId = studentId,
            SectionId = sectionId,
            DateAdded = DateOnly.FromDateTime(DateTime.Now)
        };

        await _context.AddAsync(sectionStudent);
        await _context.SaveChangesAsync();
        return true;
    }
}