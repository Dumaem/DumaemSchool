using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.Mappers.EntityMapping;

public class TeacherDtoEntityMapping : AbstractEntityMapping<TeacherDto>
{
    public TeacherDtoEntityMapping()
    {
        Map(nameof(TeacherDto.Id), "t.id", isPrimaryKey: true);
        Map(nameof(TeacherDto.Name), "t.name");
        Map(nameof(TeacherDto.IsDeleted), "t.is_deleted");
        Map(nameof(TeacherDto.SectionsCount), "count(st.*)", true);
    }
}