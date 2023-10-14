using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Database.Mappers.EntityMapping.Base;
using SectionStudent = DumaemSchool.Core.OutputModels.SectionStudent;

namespace DumaemSchool.Database.ListGetters.Impl;

public sealed class SectionStudentListSqlGetter : AbstractListSqlGenerator<SectionStudent>
{
    public SectionStudentListSqlGetter(IEntityMapping<SectionStudent> mapping) : base(mapping)
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
                           FROM public.section_student ss
                                JOIN public.section sec ON ss.section_id = sec.id
                                JOIN public.student stud ON ss.student_id = stud.id
                           WHERE is_actual {where}
                           ORDER BY {sort}",
            CountSql = $@"SELECT {SelectString} 
                           FROM public.section_student ss
                                JOIN public.section sec ON ss.section_id = sec.id
                                JOIN public.student stud ON ss.student_id = stud.id
                           WHERE is_actual {where}",
            Parameters = dynamicParams
        };
    }
}