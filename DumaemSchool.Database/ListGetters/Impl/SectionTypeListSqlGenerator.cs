using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Database.Mappers.EntityMapping.Base;
using SectionType = DumaemSchool.Core.Models.SectionType;

namespace DumaemSchool.Database.ListGetters.Impl;

public sealed class SectionTypeListSqlGenerator : AbstractListSqlGenerator<SectionType>
{
    public SectionTypeListSqlGenerator(IEntityMapping<SectionType> mapping) : base(mapping)
    {
    }

    public override ListQuery GetListSql(ListParam param)
    {
        var dynamicParams = new DynamicParameters();

        var where = GetWhereExpression(param.Filters, dynamicParams);
        var sort = GetOrderByExpression(param.Sorting);

        return new ListQuery
        {
            SelectSql = $@"SELECT {SelectString} 
                           FROM public.section_type s
                           WHERE NOT is_deleted {where}
                           ORDER BY {sort}",
            CountSql = $@"SELECT {SelectString} 
                           FROM public.section_type s
                           WHERE NOT is_deleted {where}",
            Parameters = dynamicParams
        };
    }
}