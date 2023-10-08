using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.ListGetters;
using Microsoft.EntityFrameworkCore;

namespace DumaemSchool.Database.Repositories.Impl;

public sealed class SectionRepository : ISectionRepository
{
    private readonly ApplicationContext _context;
    private readonly IListSqlGenerator<SectionInfo> _sectionInfoSqlGenerator;

    public SectionRepository(IListSqlGenerator<SectionInfo> sectionInfoSqlGenerator, 
        ApplicationContext context)
    {
        _sectionInfoSqlGenerator = sectionInfoSqlGenerator;
        _context = context;
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
}