using DumaemSchool.Database.Mappers.EntityMapping.Base;
using SectionStudent = DumaemSchool.Core.OutputModels.SectionStudent;

namespace DumaemSchool.Database.Mappers.EntityMapping;

public sealed class SectionStudentEntityMapping : AbstractEntityMapping<SectionStudent>
{
    public SectionStudentEntityMapping()
    {
        Map(nameof(SectionStudent.Id), "ss.id", isPrimaryKey: true);
        Map(nameof(SectionStudent.SectionId), "sec.id");
        Map(nameof(SectionStudent.SectionGroupName), "sec.group_name");
        Map(nameof(SectionStudent.StudentId), "stud.id");
        Map(nameof(SectionStudent.StudentName), "stud.name");
        Map(nameof(SectionStudent.DateAdded), "ss.date_added");
    }
}