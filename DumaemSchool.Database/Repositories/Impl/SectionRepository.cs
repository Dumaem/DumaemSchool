using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.ListGetters;
using Microsoft.EntityFrameworkCore;
using SectionStudent = DumaemSchool.Core.OutputModels.SectionStudent;

namespace DumaemSchool.Database.Repositories.Impl;

public sealed class SectionRepository : ISectionRepository
{
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
            Items = result, TotalItemsCount = result.Count
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
            Items = result, TotalItemsCount = result.Count
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
            Items = result, TotalItemsCount = result.Count
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
}