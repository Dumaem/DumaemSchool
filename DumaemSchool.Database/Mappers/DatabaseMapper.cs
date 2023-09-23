using Riok.Mapperly.Abstractions;

namespace DumaemSchool.Database.Mappers;

[Mapper]
public sealed partial class DatabaseMapper
{
    public partial DumaemSchool.Core.Models.Teacher Map(Teacher teacherDb);
    
    [MapperIgnoreSource(nameof(Core.Models.Teacher.Id))]
    public partial Teacher Map(DumaemSchool.Core.Models.Teacher teacher);
}