using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;

namespace DumaemSchool.Database.Repositories;

public interface ITeacherRepository
{
    public Task<ListDataResult<TeacherDto>> ListTeachersAsync(ListParam listParam);
    public Task<Core.Models.Teacher> AddTeacherAsync(Core.Models.Teacher teacher);
    public Task<Core.Models.Teacher?> GetTeacherInfo(int teacherId);
    public Task<bool> UpdateTeacherNameAsync(int teacherId, string name);
    public Task<bool> DeleteTeacherAsync(int id);
    public Task<bool> CheckTeacherAvailabilityToSection(int teacherId, List<SectionSchedule> sectionSchedule);
    public Task<bool> AddTeacherToSection(int teacherId, int sectionId, int oldTeacherId);
}