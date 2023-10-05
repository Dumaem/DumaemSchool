using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;

namespace DumaemSchool.Database.Repositories;

public interface ITeacherRepository
{
    public Task<IEnumerable<TeacherDto>> ListTeachersAsync(bool includeFired, ListParam listParam);
    public Task<Core.Models.Teacher> AddTeacherAsync(Core.Models.Teacher teacher);
    public Task<bool> DeleteTeacherAsync(int id);
}