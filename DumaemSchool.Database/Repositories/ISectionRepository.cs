using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;

namespace DumaemSchool.Database.Repositories;

public interface ISectionRepository
{
    public Task<ListDataResult<SectionInfo>> ListSectionInfo(ListParam param);
}