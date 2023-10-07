using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;

namespace DumaemSchool.Database.Repositories;

public interface ISectionTypeRepository
{
    public Task<ListDataResult<Core.Models.SectionType>> ListSectionTypesAsync(ListParam listParam);
    public Task<int> AddSectionTypeAsync(string name);
    public Task<bool> UpdateSectionTypeNameAsync(int id, string name);
    public Task<bool> DeleteSectionTypeAsync(int id);
}