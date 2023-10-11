using DumaemSchool.Core.Commands.Base;
using LanguageExt.Common;
using MediatR;

namespace DumaemSchool.Core.Commands.Teacher;

public sealed record AddTeacherCommand(Models.Teacher Teacher, string Email) : IRequest<Result<Models.Teacher>>, ITransactRequest;