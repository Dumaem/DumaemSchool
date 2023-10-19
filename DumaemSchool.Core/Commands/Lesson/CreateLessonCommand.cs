using LanguageExt.Common;
using MediatR;

namespace DumaemSchool.Core.Commands.Lesson;

public sealed record CreateLessonCommand(Models.Lesson Lesson) : IRequest<Result<Models.Lesson>>;