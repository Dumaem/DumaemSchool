using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Commands.Lesson;

public sealed record ChangeLessonStudentsActivityCommand(IEnumerable<StudentLessonStatistics> Activity) : IRequest;