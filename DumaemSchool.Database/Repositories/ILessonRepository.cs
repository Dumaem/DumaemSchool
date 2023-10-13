using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.OutputModels;
using Lesson = DumaemSchool.Core.Models.Lesson;

namespace DumaemSchool.Database.Repositories;

public interface ILessonRepository
{
    public Task<IEnumerable<LessonDate>> ListSectionLessonDates(int sectionId);
    public Task<ListDataResult<StudentLessonStatistics>> ListLessonStatistics(ListParam param);
    public Task<Lesson> CreateLesson(Lesson lesson);
}