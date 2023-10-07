using DumaemSchool.Database.Mappers.EntityMapping.Base;
using SectionType = DumaemSchool.Core.Models.SectionType;

namespace DumaemSchool.Database.Mappers.EntityMapping;

public class SectionTypeEntityMapping : AbstractEntityMapping<SectionType>
{
    public SectionTypeEntityMapping()
    {
        Map(nameof(SectionType.Id), "s.id", isPrimaryKey: true);
        Map(nameof(SectionType.Name), "s.name");
    }
}