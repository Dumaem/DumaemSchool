using Dapper;
using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.DataManipulation;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.ListGetters.Impl;

public sealed class SectionStudentsToAddListSqlGetter : AbstractListSqlGenerator<StudentToAddToSection>
{
    public SectionStudentsToAddListSqlGetter(IEntityMapping<StudentToAddToSection> mapping) : base(mapping)
    {
    }

    public override ListQuery GetListSql(ListParam param)
    {
        var dynamicParams = new DynamicParameters();

        var sectionIdFilterDefinition =
            param.Filters.First(x => x.FieldName == nameof(StudentToAddToSection.SectionId));
        var sectionIdFilter = SqlUtility.GetFilterToSql(sectionIdFilterDefinition, dynamicParams,
            new Dictionary<string, string> { { nameof(StudentToAddToSection.SectionId), "ss.section_id" } })!;

        var filters = param.Filters
            .Where(x => x != sectionIdFilterDefinition)
            .ToArray();
        var where = GetWhereExpression(filters, dynamicParams);
        var sort = GetOrderByExpression(param.Sorting);
        var pagination = GetPaginationExpression(param.Pagination);

        return new ListQuery
        {
            SelectSql = $@"WITH section_students AS (SELECT ss.student_id, ss.section_id
                                                     FROM public.section_student ss
                                                     WHERE {sectionIdFilter.SqlText})
                           SELECT {SelectString}, @{sectionIdFilter.ParameterName} ""SectionId""
                           FROM public.student s
                                LEFT JOIN section_students ss ON s.id = ss.student_id
                           WHERE ss.student_id IS NULL {where}
                           ORDER BY {sort}
                           {pagination}",
            CountSql = $@"WITH section_students AS (SELECT ss.student_id, ss.section_id
                                                     FROM public.section_student ss
                                                     WHERE {sectionIdFilter})
                           SELECT {SelectString} 
                           FROM public.student s
                                LEFT JOIN section_students ss ON s.id = ss.student_id
                           WHERE ss.student_id IS NULL {where}",
            Parameters = dynamicParams
        };
    }
}