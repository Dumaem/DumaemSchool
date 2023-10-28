using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.ListGetters;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Database.Repositories.Impl;

public sealed class SectionTypeRepository : ISectionTypeRepository
{
    private static readonly Func<ApplicationContext, IEnumerable<SectionType>> SectionTypeQuery =
        EF.CompileQuery<ApplicationContext, IEnumerable<SectionType>>(ctx =>
            from sectionType in ctx.SectionTypes
            where !sectionType.IsDeleted
            select sectionType);

    private readonly ApplicationContext _context;
    private readonly IListSqlGenerator<Core.Models.SectionType> _sqlGenerator;

    public SectionTypeRepository(ApplicationContext context, 
        IListSqlGenerator<Core.Models.SectionType> sqlGenerator)
    {
        _context = context;
        _sqlGenerator = sqlGenerator;
    }

    public async Task<ListDataResult<Core.Models.SectionType>> ListSectionTypesAsync(ListParam listParam)
    {
        var listQuery = _sqlGenerator.GetListSql(listParam);
        var connection = _context.Database.GetDbConnection();
        var result = (await connection
            .QueryAsync<Core.Models.SectionType>(listQuery.SelectSql, listQuery.Parameters))
            .AsList();

        return new ListDataResult<Core.Models.SectionType>
        {
            Items = result, TotalItemsCount = result.Count
        };
    }

    public async Task<int> AddSectionTypeAsync(string name)
    {
        var sectionType = new SectionType { Name = name };
        await _context.AddAsync(sectionType);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        return sectionType.Id;
    }

    public async Task<bool> UpdateSectionTypeNameAsync(int id, string name)
    {
        var sectionType = await _context.SectionTypes.FindAsync(id);
        if (sectionType is null)
            return false;

        sectionType.Name = name;
        _context.Update(sectionType);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        return true;
    }

    public async Task<bool> DeleteSectionTypeAsync(int id)
    {
        var sectionType = await _context.SectionTypes.FindAsync(id);
        if (sectionType is null)
            return false;

        // Удаление кружков этого вида
        var deleteDate = DateOnly.FromDateTime(DateTime.Now);
        await _context.Sections
            .Where(x => x.SectionTypeId == id)
            .ExecuteUpdateAsync(props =>
                props.SetProperty(b => b.IsDeleted, true)
                    .SetProperty(b => b.DateDeleted, deleteDate)
            );

        sectionType.IsDeleted = true;
        sectionType.DateDeleted = deleteDate;
        _context.SectionTypes.Update(sectionType);
        await _context.SaveChangesAsync();
        _context.ChangeTracker.Clear();
        return true;
    }
}