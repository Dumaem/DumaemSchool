namespace DumaemSchool.Database.Repositories;

public interface ISectionRepository
{
    public Task<IEnumerable<Core.Models.SectionType>> ListSectionTypesAsync();
    public Task<int> AddSectionTypeAsync(string name);
    public Task<bool> UpdateSectionTypeNameAsync(int id, string name);
    public Task<bool> DeleteSectionTypeAsync(int id);
}