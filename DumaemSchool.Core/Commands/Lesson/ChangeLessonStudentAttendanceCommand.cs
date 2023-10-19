using DumaemSchool.Core.Models;
using DumaemSchool.Core.OutputModels;
using MediatR;

namespace DumaemSchool.Core.Commands.Lesson;

public sealed record ChangeLessonStudentAttendanceCommand(int LessonId, int StudentId, bool WasAttended) : IRequest;