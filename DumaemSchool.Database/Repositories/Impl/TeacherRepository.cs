using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.ListGetters;
using DumaemSchool.Database.Mappers;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<TeacherDto>> ListTeachersAsync(bool includeFired, ListParam param)
    {
        var listQuery = _sqlGenerator.GetListSql(param);
        var result = await _context.Database.GetDbConnection()
            .QueryAsync<TeacherDto>(listQuery.Sql, listQuery.Parameters);

        return result;
    }

    public async Task<Core.Models.Teacher> AddTeacherAsync(Core.Models.Teacher teacher)
    {
        var teacherDb = _mapper.Map(teacher);
        await _context.Teachers.AddAsync(teacherDb);
        await _context.SaveChangesAsync();
        return _mapper.Map(teacherDb);
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
}