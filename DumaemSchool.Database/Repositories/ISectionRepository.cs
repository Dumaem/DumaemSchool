using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using SectionStudent = DumaemSchool.Core.OutputModels.SectionStudent;

namespace DumaemSchool.Database.Repositories;

public interface ISectionRepository
{
    public Task<ListDataResult<SectionInfo>> ListSectionInfo(ListParam param);
    public Task<ListDataResult<SectionStudent>> ListSectionStudents(ListParam param);
}