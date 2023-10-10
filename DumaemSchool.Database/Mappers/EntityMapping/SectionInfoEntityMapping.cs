using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.Mappers.EntityMapping;

public sealed class SectionInfoEntityMapping : AbstractEntityMapping<SectionInfo>
{
    public SectionInfoEntityMapping()
    {
        Map(nameof(SectionInfo.Id), "s.id", isPrimaryKey: true);
        Map(nameof(SectionInfo.GroupName), "s.group_name");
        Map(nameof(SectionInfo.TypeName), "stype.name");
        Map(nameof(SectionInfo.TeacherName), "t.name");
        Map(nameof(SectionInfo.TeacherId), "steacher.teacher_id");
        Map(nameof(SectionInfo.LessonPerWeek), "count(schedule.*)", isAggregate: true);
    }
}