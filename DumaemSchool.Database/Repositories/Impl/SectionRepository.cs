using DumaemSchool.Database.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Database.Repositories.Impl;

public sealed class SectionRepository : ISectionRepository
{
    private static readonly Func<ApplicationContext, Task<IEnumerable<SectionType>>> SectionTypeQuery =
        EF.CompileAsyncQuery<ApplicationContext, IEnumerable<SectionType>>(ctx =>
            from sectionType in ctx.SectionTypes
            where !sectionType.IsDeleted
            select sectionType);

    private readonly ApplicationContext _context;
    private readonly DatabaseMapper _mapper;

    public SectionRepository(ApplicationContext context)
    {
        _context = context;
        _mapper = new DatabaseMapper();
    }

    public async Task<IEnumerable<Core.Models.SectionType>> ListSectionTypesAsync()
    {
        return (await SectionTypeQuery
                .Invoke(_context))
            .Select(x => _mapper.Map(x));
    }

    public async Task<int> AddSectionTypeAsync(string name)
    {
        var sectionType = new SectionType { Name = name };
        await _context.AddAsync(sectionType);
        await _context.SaveChangesAsync();
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
        return true;
    }
}