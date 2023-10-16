using DumaemSchool.Core.OutputModels;
using DumaemSchool.Database.Mappers.EntityMapping.Base;

namespace DumaemSchool.Database.Mappers.EntityMapping;

public sealed class StudentToAddToSectionEntityMapping : AbstractEntityMapping<StudentToAddToSection>
{
    public StudentToAddToSectionEntityMapping()
    {
        Map(nameof(StudentToAddToSection.Id), "s.id", isPrimaryKey: true);
        Map(nameof(StudentToAddToSection.Name), "s.name");
    }
}