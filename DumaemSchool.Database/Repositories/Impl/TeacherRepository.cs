using System.Text;
using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Database.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace DumaemSchool.Database.Repositories.Impl;

public sealed class TeacherRepository : ITeacherRepository
{
    private readonly Dictionary<string, string> _propertyToDatabaseMapping;
    private readonly ApplicationContext _context;
    private readonly DatabaseMapper _mapper;

    public TeacherRepository(ApplicationContext context)
    {
        _context = context;
        _mapper = new DatabaseMapper();
        _propertyToDatabaseMapping = new Dictionary<string, string>();
        var properties = _context.Model.FindEntityType(typeof(Teacher))!.GetProperties();
        foreach (var p in properties)
        {
            _propertyToDatabaseMapping.Add(p.Name, p.GetColumnName());
        }
    }

    public async Task<IEnumerable<Core.Models.Teacher>> ListTeachersAsync(bool includeFired, ListParam param)
    {
        const string sqlBase = "SELECT * FROM public.teacher";
        var additionalBuilder = new StringBuilder(" WHERE TRUE ");
        if (!includeFired)
            additionalBuilder.Append(" AND NOT is_deleted ");

        if (param.Filters.Any())
            additionalBuilder.Append(
                $" AND {string.Join(" AND ", param.Filters.Select(x => SqlUtility.GetFilterToSql(x, _propertyToDatabaseMapping)))} ");

        if (param.Sorting.Any())
            additionalBuilder.Append(
                $" ORDER BY {string.Join(", ", param.Sorting.Select(x => SqlUtility.GetSortingToSql(x, _propertyToDatabaseMapping)))} ");
        else
            additionalBuilder.Append(" ORDER BY id ");

        additionalBuilder.Append(
            $" LIMIT {param.Pagination.ItemCount} OFFSET {param.Pagination.PageNumber * param.Pagination.ItemCount} ");

        var result = await _context.Database.GetDbConnection().QueryAsync<Teacher>($"{sqlBase} {additionalBuilder}");

        return result.Select(x => _mapper.Map(x));
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