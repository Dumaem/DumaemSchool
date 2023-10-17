using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.Models;
using DumaemSchool.Core.OutputModels;
using Section = DumaemSchool.Core.Models.Section;
using SectionStudent = DumaemSchool.Core.OutputModels.SectionStudent;

namespace DumaemSchool.Database.Repositories;

public interface ISectionRepository
{
    public Task<ListDataResult<SectionInfo>> ListSectionInfo(ListParam param);
    public Task<ListDataResult<SectionStudent>> ListSectionStudents(ListParam param);
    public Task<ListDataResult<SectionSchedule>> ListSectionSchedule(ListParam param);
    public Task<ListDataResult<StudentToAddToSection>> ListStudentsToAdd(ListParam param);
    public Task<bool> DeleteStudentFromSection(int studentId, int sectionId);
    public Task<bool> AddStudentToSection(int studentId, int sectionId);
    public Task<Section> CreateSection(SectionWithSchedule section);
    public Task<bool> CheckStudentAvailabilityToSection(int studentId, List<SectionSchedule> sectionSchedule);
    public Task<TeacherDto?> GetTeacherFromSection(int sectionId);
}