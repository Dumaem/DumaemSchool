using DumaemSchool.Core.Models;
using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Commands.Lesson;

public sealed record ChangeLessonStudentActivityCommand(int LessonId, int StudentId, LessonActivityMark Mark) : IRequest;