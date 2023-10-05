using DumaemSchool.Core.DataManipulation;

namespace DumaemSchool.Database.Repositories;

public interface ITeacherRepository
{
    public Task<IEnumerable<Core.Models.Teacher>> ListTeachersAsync(bool includeFired, ListParam listParam);
    public Task<Core.Models.Teacher> AddTeacherAsync(Core.Models.Teacher teacher);
    public Task<bool> DeleteTeacherAsync(int id);
}