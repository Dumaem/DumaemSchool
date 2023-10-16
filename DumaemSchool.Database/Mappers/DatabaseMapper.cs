using DumaemSchool.Core.OutputModels;
using Riok.Mapperly.Abstractions;

namespace DumaemSchool.Database.Mappers;

[Mapper]
public sealed partial class DatabaseMapper
{
    public partial DumaemSchool.Core.Models.Teacher Map(Teacher teacherDb);

    [MapperIgnoreSource(nameof(Core.Models.Teacher.Id))]
    public partial Teacher Map(DumaemSchool.Core.Models.Teacher teacher);

    public partial DumaemSchool.Core.Models.SectionType Map(SectionType sectionType);
    public partial DumaemSchool.Core.Models.Lesson Map(Lesson lesson);
    
    public partial Lesson Map(DumaemSchool.Core.Models.Lesson lesson);
}