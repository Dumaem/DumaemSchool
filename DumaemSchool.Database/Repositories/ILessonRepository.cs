﻿using DumaemSchool.Core.DataManipulation;
using DumaemSchool.Core.Models;
using DumaemSchool.Core.OutputModels;
using Lesson = DumaemSchool.Core.Models.Lesson;

namespace DumaemSchool.Database.Repositories;

public interface ILessonRepository
{
    public Task<IEnumerable<LessonDate>> ListSectionLessonDates(int sectionId);
    public Task<ListDataResult<StudentLessonStatistics>> ListLessonStatistics(ListParam param);
    public Task<ListDataResult<LessonForScheduler>> ListTeacherLessonSchedule(ListParam param);
    public Task<Lesson> CreateLesson(Lesson lesson);
    public Task ChangeLessonStudentActivity (int lessonId, int studentId, LessonActivityMark mark);
    public Task ChangeLessonStudentAttendance (int lessonId, int studentId, bool wasAttended);
}